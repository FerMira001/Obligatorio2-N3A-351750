using Microsoft.AspNetCore.Mvc;
using LogicaApp.InterfacesCasosDeUso.Pago; 
using LogicaApp.DTO;

namespace TuProyecto.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagoApiController : ControllerBase
    {
        private IObtenerPago _obtenerPago;

        public PagoApiController(IObtenerPago obtenerPago)
        {
            _obtenerPago = obtenerPago;
        }

        [HttpGet("{id}")]
        public PagoDTO Get(int id)
        {
            PagoDTO pago;
            try
            {
                pago = _obtenerPago.ObtenerPago(id);
            }
            catch
            {
                return null;
            }

            return pago;
        }
    }
}
