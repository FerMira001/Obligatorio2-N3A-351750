using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.Pago;
using LogicaApp.InterfacesCasosDeUso.PagoRecurrente;
using LogicaApp.InterfacesCasosDeUso.PagoUnico;
using LogicaApp.InterfacesCasosDeUso.TipoGasto;
using LogicaApp.InterfacesCasosDeUso.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Obligatorio1.Controllers
{
    public class PagoController : Controller
    {
        private IObtenerTipoGasto _obtenerTipoGasto;
        private IObtenerUsuario _obtenerUsuario;
        private IAgregarPagoUnico _agregarPagoUnico;
        private IAgregarPagoRecurrente _agregarPagoRecurrente;
        private IListarTiposDeGasto _listarTiposDeGasto;
        private IListarPagosUnicosMes _listarPagosUnicosMes;
        private IListarPagosRecMes _listarPagosRecMes;
        private IObtenerMontoDeUsuario _obtenerMontoDeUsuario;
        private IListarUsuarios _listarUsuarios;

        public PagoController(
            IObtenerTipoGasto obtenerTipoGasto,
            IObtenerUsuario obtenerUsuario,
            IAgregarPagoUnico agregarPagoUnico,
            IAgregarPagoRecurrente agregarPagoRecurrente,
            IListarTiposDeGasto listarTiposDeGasto,
            IListarPagosUnicosMes listarPagosUnicosMes,
            IListarPagosRecMes listarPagosRecMes,
            IObtenerMontoDeUsuario obtenerMontoDeUsuario,
            IListarUsuarios listarUsuarios
        )
        {
            _obtenerTipoGasto = obtenerTipoGasto;
            _obtenerUsuario = obtenerUsuario;
            _agregarPagoUnico = agregarPagoUnico;
            _agregarPagoRecurrente = agregarPagoRecurrente;
            _listarTiposDeGasto = listarTiposDeGasto;
            _listarPagosUnicosMes = listarPagosUnicosMes;
            _listarPagosRecMes = listarPagosRecMes;
            _obtenerMontoDeUsuario = obtenerMontoDeUsuario;
            _listarUsuarios = listarUsuarios;
        }


        // GET: PagoController
        public ActionResult ListarPagos(int? mes, int? anio)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "1")
                return RedirectToAction("Home", "Index");
            if (mes == null)
            {
                mes = DateTime.Now.Month;
            }
            if (anio == null)
            {
                anio = DateTime.Now.Year;
            }
            ViewBag.mes = mes.ToString();
            ViewBag.anio = anio.ToString();
            var pagosUnicos = _listarPagosUnicosMes.ListarPagosUnicosMes(mes.Value, anio.Value).ToList();
            var pagosRecurrentes = _listarPagosRecMes.ListarPagosRecMes(mes.Value, anio.Value).ToList();

            decimal totalAPagar = 0;
            foreach (PagoRecurrenteDTO pago in pagosRecurrentes)
            {
                totalAPagar += pago.MontoMensual * (((pago.FechaFin.Year - anio.Value) * 12) + pago.FechaFin.Month - mes.Value);
            }

            ViewBag.TotalAPagar = totalAPagar;

            var pagosUnicosSinId = pagosUnicos.Select(p => new
            {
                p.Id,
                MetodoPago = p.MetodoPago == 0 ? "Crédito" : p.MetodoPago == 1 ? "Efectivo":"",
                TipoGasto = _obtenerTipoGasto.ObtenerTipoGasto(p.TipoGastoId).Nombre,
                Usuario = _obtenerUsuario.ObtenerUsuario(p.UsuarioId).Nombre,
                p.Desc,
                p.FechaPago,
                p.NumRecibo,
                p.Monto
            });

            var pagosRecSinId = pagosRecurrentes.Select(p => new
            {
                p.Id,
                MetodoPago = p.MetodoPago == 0 ? "Crédito" : p.MetodoPago == 1 ? "Efectivo":"",
                TipoGasto = _obtenerTipoGasto.ObtenerTipoGasto(p.TipoGastoId).Nombre,
                Usuario = _obtenerUsuario.ObtenerUsuario(p.UsuarioId).Nombre,
                p.Desc,
                p.FechaInicio,
                p.FechaFin,
                p.MontoMensual
            });

            ViewBag.PagosUnicos = pagosUnicosSinId;
            ViewBag.PagosRecurrentes = pagosRecSinId;
            return View();
        }

        public IActionResult MostrarPagosUsuarios(decimal? montoMin)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "1")
                return RedirectToAction("Home", "Index");

            decimal montoMinimo = 0;
            if (montoMin != null)
            {
                montoMinimo = montoMin.Value;
            }
            ViewBag.MontoMinimo = montoMinimo;


            var usuarios = _listarUsuarios.ListarUsuarios().ToList();

            var usuariosConPagos = new List<Object>();

            foreach (UsuarioDTO u in usuarios)
            {
                // Obtener monto total del usuario usando tu caso de uso
                decimal totalPagado = _obtenerMontoDeUsuario.ObtenerMontoDeUsuario(u.Id);

                // Solo agregamos si supera el monto mínimo
                if (totalPagado > montoMinimo)
                {
                    usuariosConPagos.Add(new
                    {
                        Usuario = u.Nombre + " " + u.Apellido,
                        Mail = u.Mail,
                        TotalPagado = totalPagado
                    });
                }
            }
            ViewBag.Usuarios = usuariosConPagos;
            return View();
        }


        // GET: PagoController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
                return RedirectToAction("Usuario", "Login");
            ViewBag.TiposGasto = _listarTiposDeGasto.ListarTiposDeGasto();
            return View();
        }

        /*
        Se le pidió a chatGPT que generara el código para este método Create
        Prompt: "Hazme el codigo de Create en el controller de pagosController en base a esto: <dto de pago, pago recurrente y pago unico>"
         */
        // POST: PagoController/Create
        [HttpPost]
        public IActionResult Create(
            string tipoPago,
            int metodoPago,
            int tipoGastoId,
            string desc,

            // Campos de pago único
            DateTime? fechaPago,
            string numRecibo,
            decimal? monto,

            // Campos de pago recurrente
            DateTime? fechaInicio,
            DateTime? fechaFin,
            decimal? montoMensual
        )
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
                return RedirectToAction("Usuario", "Login");
            try
            {
                int usuarioId;
                int.TryParse(HttpContext.Session.GetString("UsuarioId"), out usuarioId);
                if (tipoPago == "Unico")
                {
                    var dto = new PagoUnicoDTO
                    {
                        MetodoPago = metodoPago,
                        TipoGastoId = tipoGastoId,
                        UsuarioId = usuarioId,
                        Desc = desc,
                        FechaPago = fechaPago.Value,
                        NumRecibo = numRecibo,
                        Monto = monto.Value
                    };

                    _agregarPagoUnico.AgregarPagoUnico(dto);
                }
                else if (tipoPago == "Recurrente")
                {
                    var dto = new PagoRecurrenteDTO
                    {
                        MetodoPago = metodoPago,
                        TipoGastoId = tipoGastoId,
                        UsuarioId = usuarioId,
                        Desc = desc,
                        FechaInicio = fechaInicio.Value,
                        FechaFin = fechaFin.Value,
                        MontoMensual = montoMensual.Value
                    };

                    _agregarPagoRecurrente.AgregarPagoRecurrente(dto);
                }
                else
                {
                    ViewBag.Error = "Debe seleccionar un tipo de pago.";
                    return View();
                }

                TempData["Exito"] = "Pago registrado correctamente";
                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }
        }


        // GET: PagoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PagoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PagoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PagoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
