using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.Usuario;
using Estructura.Excepciones;
using LogicaApp.CasosDeUso.Usuario;
using LogicaApp.InterfacesCasosDeUso.Equipo;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;

namespace Obligatorio1.Controllers
{
    public class UsuarioController : Controller
    {
        private const string DOMINIO_EMPRESA = "@laEmpresa.com";

        private Random rand = new Random();

        private IObtenerUsuarioPorMail obtenerUsuarioPorMail;
        private IListarEquipos listarEquipos;
        private IObtenerEquipo obtenerEquipo;
        private IAgregarUsuario agregarUsuario;

        public UsuarioController(
            IObtenerUsuarioPorMail obtenerUsuarioPorMail,
            IListarEquipos listarEquipos,
            IObtenerEquipo obtenerEquipo,
            IAgregarUsuario agregarUsuario
            )
        {
            this.obtenerUsuarioPorMail = obtenerUsuarioPorMail;
            this.listarEquipos = listarEquipos;
            this.obtenerEquipo = obtenerEquipo;
            this.agregarUsuario = agregarUsuario;
        }

        // GET: Login
        public ActionResult Login()
        {
            // Si ya hay sesión activa, redirigir al home
            if (HttpContext.Session.GetString("UsuarioMail") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(string mail, string pass)
        {
            if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(pass))
            {
                ViewBag.Error = "Debe completar todos los campos.";
                return View();
            }

            try
            {
                UsuarioDTO usuario = obtenerUsuarioPorMail.ObtenerUsuarioPorMail(mail);

                if (usuario != null && usuario.Password == pass)
                {
                    
                    HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
                    HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
                    HttpContext.Session.SetString("UsuarioMail", usuario.Mail);
                    HttpContext.Session.SetString("UsuarioRol", usuario.Rol.ToString());

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Credenciales incorrectas.";
                    return View();
                }
            }
            catch (UsuarioException ex)
            {
                ViewBag.Error = "Error al intentar iniciar sesión: " + ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error inesperado: " + ex.Message;
                return View();
            }
        }

        public IActionResult Registrar()
        {
            //Por si el usuario no es admin
            if (HttpContext.Session.GetString("UsuarioRol") != "0")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                ViewBag.Equipos = listarEquipos.ListarEquipos();
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Ocurrió un error al cargar los equipos: " + ex.Message;
                return View();
            }

            return View();
        }
        
        [HttpPost]
        public IActionResult Registrar(string nombre, string apellido, string pass, int equipoId, string rol)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "0")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                int idRol;
                int.TryParse(rol, out idRol);

                EquipoDTO equipo = obtenerEquipo.ObtenerEquipo(equipoId);

                UsuarioDTO nuevoUser = new UsuarioDTO(nombre, apellido, GenerarMail(nombre, apellido), pass, equipo, idRol);

                agregarUsuario.AgregarUsuario(nuevoUser);
                ViewBag.Exito = "Usuario registrado con éxito.";
                return View();
            }
            catch(UsuarioException ex)
            {
                ViewBag.Error = "Error al registrar el usuario: " + ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error inesperado: " + ex.Message;
                return View();
            }
        }
        

        private string GenerarMail(string nombre, string apellido)
        {
            nombre = LimpiarString(nombre);
            apellido = LimpiarString(apellido);
            if (nombre.IsNullOrEmpty() || apellido.IsNullOrEmpty())
            {
                throw new UsuarioException("Error al generar el correo. Nombre o apellido vacío.");
            }
            string res = nombre.Substring(0, Math.Min(3, nombre.Length));
            res += apellido.Substring(0, Math.Min(3, apellido.Length));
            UsuarioDTO usuarioBuscado = obtenerUsuarioPorMail.ObtenerUsuarioPorMail(res + DOMINIO_EMPRESA);
            if(usuarioBuscado != null)
            {
                int random = rand.Next(1000, 10000);
                res += random.ToString();
            }
            res += DOMINIO_EMPRESA;
            return res;
        }

        /*
        Funcion LimpiarString pedida a Claude.ia
        Prompt:
        "Creame la función 

        private string LimpiarString(string texto)

        De forma que Los caracteres con tildes y otras alteraciones 
        (eñes, vocales con tildes, diéresis, etc.) deberán remplazarse
        por sus versiones sin la alteración."
         */
        private string LimpiarString(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            // Normalizar el texto a FormD (descompone caracteres con tildes)
            string textoNormalizado = texto.Normalize(NormalizationForm.FormD);

            StringBuilder sb = new StringBuilder();

            foreach (char c in textoNormalizado)
            {
                // Obtener la categoría Unicode del caracter
                UnicodeCategory categoria = CharUnicodeInfo.GetUnicodeCategory(c);

                // Si no es un carácter de marca no espaciadora (tildes, diéresis, etc.)
                if (categoria != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            // Normalizar de vuelta a FormC y manejar casos especiales
            string resultado = sb.ToString().Normalize(NormalizationForm.FormC);

            // Reemplazar caracteres especiales manualmente (ñ, Ñ, etc.)
            resultado = resultado.Replace("ñ", "n")
                                 .Replace("Ñ", "N")
                                 .Replace("ç", "c")
                                 .Replace("Ç", "C");

            return resultado;
        }



        // GET: Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }
    }
}