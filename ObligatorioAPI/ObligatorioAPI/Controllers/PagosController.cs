using Microsoft.AspNetCore.Mvc;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.PagoUnico;
using LogicaApp.InterfacesCasosDeUso.PagoRecurrente;
using Microsoft.AspNetCore.Authorization; 
using System.Security.Claims;

namespace ObligatorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IListarPagosUnicosPorUsuario _listarPagosUnicos;
        private readonly IListarPagosRecurrentesPorUsuario _listarPagosRecurrentes;
        private readonly IAgregarPagoUnico _agregarPagoUnico;
        private readonly IAgregarPagoRecurrente _agregarPagoRecurrente;

        public PagosController(
            IListarPagosUnicosPorUsuario listarPagosUnicos,
            IListarPagosRecurrentesPorUsuario listarPagosRecurrentes,
            IAgregarPagoUnico agregarPagoUnico,
            IAgregarPagoRecurrente agregarPagoRecurrente)
        {
            _listarPagosUnicos = listarPagosUnicos;
            _listarPagosRecurrentes = listarPagosRecurrentes;
            _agregarPagoUnico = agregarPagoUnico;
            _agregarPagoRecurrente = agregarPagoRecurrente;
        }

        [HttpGet("usuario/{usuarioId}")]
        [Authorize(Roles = "Empleado,Gerente")]
        public IActionResult ObtenerPagosPorUsuario(int usuarioId)
        {
            try
            {
                // Obtener el id del usuario del token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userIdClaim == null)
                {
                    return Unauthorized(new { mensaje = "Token inválido" });
                }

                int userIdToken = int.Parse(userIdClaim);

                // Verificar que el usuario solo pueda ver sus propios pagos
                if (userIdToken != usuarioId)
                {
                    return Forbid();
                }

                var pagosUnicos = _listarPagosUnicos.ListarPagosUnicosPorUsuario(usuarioId);
                var pagosRecurrentes = _listarPagosRecurrentes.ListarPagosRecurrentesPorUsuario(usuarioId);

                return Ok(new
                {
                    pagosUnicos,
                    pagosRecurrentes
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        [HttpPost("unico")]
        [Authorize(Roles = "Empleado,Gerente,Empleado")]
        public IActionResult AgregarPagoUnico([FromBody] PagoUnicoDTO pago)
        {
            try
            {
                _agregarPagoUnico.AgregarPagoUnico(pago);
                return Ok(new { mensaje = "Pago único creado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("recurrente")]
        [Authorize(Roles = "Empleado,Gerente,Empleado")]
        public IActionResult AgregarPagoRecurrente([FromBody] PagoRecurrenteDTO pago)
        {
            try
            {
                _agregarPagoRecurrente.AgregarPagoRecurrente(pago);
                return Ok(new { mensaje = "Pago recurrente creado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}