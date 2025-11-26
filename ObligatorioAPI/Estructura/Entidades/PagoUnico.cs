using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Entidades
{
    public class PagoUnico : Pago
    {
        public DateTime fechaPago { get; set; }
        public string numRecibo { get; set; }
        public decimal monto { get; set; }
    }
}
