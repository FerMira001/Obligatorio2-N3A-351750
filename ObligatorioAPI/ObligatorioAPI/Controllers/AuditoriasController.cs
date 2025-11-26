using Microsoft.AspNetCore.Mvc;
using LogicaApp.InterfacesCasosDeUso.Auditoria;
using Microsoft.AspNetCore.Authorization;

namespace ObligatorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriasController : ControllerBase
    {
        private readonly IListarAuditoriasPorTipoGasto _listarAuditorias;

        public AuditoriasController(IListarAuditoriasPorTipoGasto listarAuditorias)
        {
            _listarAuditorias = listarAuditorias;
        }

        [HttpGet("tipogasto/{tipoGastoId}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult ObtenerAuditoriasPorTipoGasto(int tipoGastoId)
        {
            try
            {
                var auditorias = _listarAuditorias.ListarAuditoriasPorTipoGasto(tipoGastoId);
                return Ok(auditorias);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}