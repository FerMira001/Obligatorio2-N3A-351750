using LogicaApp.InterfacesCasosDeUso.TipoGasto;
using LogicaApp.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Estructura.Excepciones;
using LogicaApp.InterfacesCasosDeUso.Pago;
using LogicaApp.InterfacesCasosDeUso.Auditoria;
using Humanizer;

namespace Obligatorio1.Controllers
{
    public class TipoGastoController : Controller
    {
        private IListarTiposDeGasto _listarTipos;
        private IAgregarTipoGasto _agregarTipo;
        private IActualizarTipoGasto _actualizarTipo;
        private IObtenerTipoGasto _obtenerTipo;
        private IObtenerTipoGastoPorNombre _obtenerTipoGastoPorNombre;
        private IQuitarTipoGasto _quitarTipo;
        private ITipoGastoEnUso _tipoGastoEnUso;
        private IAgregarAuditoria _agregarAuditoria;

        public TipoGastoController(
            IListarTiposDeGasto listarTipos,
            IAgregarTipoGasto agregarTipo,
            IActualizarTipoGasto actualizarTipo,
            IObtenerTipoGasto obtenerTipo,
            IQuitarTipoGasto quitarTipo,
            ITipoGastoEnUso tipoGastoEnUso,
            IAgregarAuditoria agregarAuditoria,
            IObtenerTipoGastoPorNombre obtenerTipoGastoPorNombre)
        {
            _listarTipos = listarTipos;
            _agregarTipo = agregarTipo;
            _actualizarTipo = actualizarTipo;
            _obtenerTipo = obtenerTipo;
            _quitarTipo = quitarTipo;
            _tipoGastoEnUso = tipoGastoEnUso;
            _agregarAuditoria = agregarAuditoria;
            _obtenerTipoGastoPorNombre = obtenerTipoGastoPorNombre;
        }

        // GET: TipoGasto
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "0")
                return RedirectToAction("Index", "Home");

            try
            {
                var tipos = _listarTipos.ListarTiposDeGasto();
                return View(tipos);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se pudo listar los tipos de gasto: " + ex.Message;
                return View();
            }
        }

        private void RegistrarAuditoria(int idTipoGasto, AuditoriaDTO.AccionEnum accion)
        {
            int idUsuario = int.Parse(HttpContext.Session.GetString("UsuarioId"));
            AuditoriaDTO audi = new AuditoriaDTO(idTipoGasto, accion, idUsuario);
            _agregarAuditoria.AgregarAuditoria(audi);
        }

        // GET: TipoGasto/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "0")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: TipoGasto/Create
        [HttpPost]
        public ActionResult Create(string nombre, string desc)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "0")
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                TipoGastoDTO dto = new TipoGastoDTO(nombre, desc);
                _agregarTipo.AgregarTipoGasto(dto);

                TipoGastoDTO tipoAgregado = _obtenerTipoGastoPorNombre.ObtenerTipoGastoPorNombre(nombre);
                RegistrarAuditoria(tipoAgregado.Id, AuditoriaDTO.AccionEnum.ADICION);

                ViewBag.Exito = "Tipo de gasto agregado con éxito.";
                return View();
            }
            catch (TipoGastoException ex)
            {
                ViewBag.Error = "Error al agregar tipo de gasto: " + ex.Message;
                return View();
            }
            catch (AuditoriaException ex)
            {
                ViewBag.Error = "Hay un error en el sistema de guargado de registros de auditoría: " + ex.Message;
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Error inesperado: " + ex.Message;
                return View();
            }
        }

        // GET: TipoGasto/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "0")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var tipo = _obtenerTipo.ObtenerTipoGasto(id);
                return View(tipo);
            }
            catch(TipoGastoException ex) 
            {
                ViewBag.Error = "Error al obtener el tipo de gasto: " + ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error inesperado: " + ex.Message;
                return View();
            }
        }

        // POST: TipoGasto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, string nombre, string desc)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "0")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                TipoGastoDTO dto = new TipoGastoDTO(nombre, desc);
                dto.Id = id;
                _actualizarTipo.ActualizarTipoGasto(dto);

                RegistrarAuditoria(id, AuditoriaDTO.AccionEnum.EDICION);

                ViewBag.Exito = "Tipo de gasto actualizado correctamente.";
                return View();
            }
            catch (TipoGastoException ex)
            {
                ViewBag.Error = "Error al actualizar tipo de gasto: " + ex.Message;
                return View();
            }
            catch (AuditoriaException ex)
            {
                ViewBag.Error = "Hay un error en el sistema de guargado de registros de auditoría: " + ex.Message;
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Error inesperado: " + ex.Message;
                return View();
            }
        }

        // GET: TipoGasto/Delete/5
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "0")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var tipo = _obtenerTipo.ObtenerTipoGasto(id);
                if (_tipoGastoEnUso.TipoGastoEnUso(id))
                {
                    TempData["Error"] = "No se puede eliminar un tipo de gasto que está en uso.";
                    return RedirectToAction("Index");
                }
                return View(tipo);
            }
            catch(TipoGastoException ex)
            {
                ViewBag.Error = "Error al obtener el tipo de gasto: " + ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error inesperado: " + ex.Message;
                return View();
            }
        }

        // POST: TipoGasto/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "0")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                _quitarTipo.QuitarTipoGasto(id);

                RegistrarAuditoria(id, AuditoriaDTO.AccionEnum.ELIMINACION);

                TempData["Exito"] = "Tipo de gasto eliminado correctamente.";
                return RedirectToAction("Index");
            }
            catch (TipoGastoException ex)
            {
                ViewBag.Error = "Error al eliminar tipo de gasto: " + ex.Message;
                return View();
            }
            catch (AuditoriaException ex)
            {
                ViewBag.Error = "Hay un error en el sistema de guargado de registros de auditoría: " + ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error inesperado: " + ex.Message;
                return View();
            }
        }

        // GET: TipoGasto/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var tipo = _obtenerTipo.ObtenerTipoGasto(id);
                return View(tipo);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al obtener el tipo de gasto: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
