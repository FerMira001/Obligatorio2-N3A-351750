using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Entidades
{
    public class PagoRecurrente : Pago
    {
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public decimal montoMensual { get; set; }

    }
}
