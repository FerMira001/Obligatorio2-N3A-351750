using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioMVC.Models
{
    public class PagoUnicoDTO : PagoDTO
    {
        public DateTime FechaPago { get; set; }
        public string NumRecibo { get; set; }
        public decimal Monto { get; set; }
    }
}