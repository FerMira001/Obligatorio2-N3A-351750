using Microsoft.AspNetCore.Mvc;
using LogicaApp.InterfacesCasosDeUso.Equipo;
using Microsoft.AspNetCore.Authorization;

namespace ObligatorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly IListarEquiposConPagosUnicosMayorA _listarEquipos;

        public EquiposController(IListarEquiposConPagosUnicosMayorA listarEquipos)
        {
            _listarEquipos = listarEquipos;
        }

        [HttpGet("pagos-mayores/{monto}")]
        [Authorize(Roles = "Gerente")]
        public IActionResult ObtenerEquiposConPagosMayorA(decimal monto)
        {
            try
            {
                var equipos = _listarEquipos.ListarEquiposConPagosUnicosMayorA(monto);
                return Ok(equipos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}