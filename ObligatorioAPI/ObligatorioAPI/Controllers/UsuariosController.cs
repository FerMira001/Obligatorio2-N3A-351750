using Microsoft.AspNetCore.Mvc;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.Usuario;
using Microsoft.AspNetCore.Authorization;

namespace ObligatorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IObtenerUsuario _obtenerUsuario;
        private readonly IActualizarUsuario _actualizarUsuario;

        public UsuariosController(IObtenerUsuario obtenerUsuario, IActualizarUsuario actualizarUsuario)
        {
            _obtenerUsuario = obtenerUsuario;
            _actualizarUsuario = actualizarUsuario;
        }

        [HttpPost("resetpassword/{usuarioId}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult ResetearPassword(int usuarioId)
        {
            try
            {
                var usuario = _obtenerUsuario.ObtenerUsuario(usuarioId);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = "Usuario no encontrado" });
                }

                // Generar nueva contraseña
                var nuevaPassword = GenerarPasswordAleatoria();

                // Actualizar usuario
                usuario.Password = nuevaPassword;
                _actualizarUsuario.ActualizarUsuario(usuario);

                return Ok(new { nuevaPassword });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        // se le pidió a chatCPT el generador de claves aleatorias
        // Se comenzó con el prompt "Como puedo crear una contraseña random de 8 caracteres que contenga al menos una letra y un numero en C#?"
        // Link al chat: https://chatgpt.com/share/6924de82-f648-8005-b137-a680ce8fe200
        private string GenerarPasswordAleatoria()
        {
            // Mínimo 8 caracteres, al menos un número y al menos una letra
            var random = new Random();
            var letras = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var numeros = "0123456789";

            // Asegurar al menos 1 letra y 1 número
            var password = "";
            password += letras[random.Next(letras.Length)];
            password += numeros[random.Next(numeros.Length)];

            // Completar hasta 8 caracteres
            var todos = letras + numeros;
            for (int i = 0; i < 6; i++)
            {
                password += todos[random.Next(todos.Length)];
            }

            // Mezclar los caracteres
            return new string(password.OrderBy(c => random.Next()).ToArray());
        }
    }
}