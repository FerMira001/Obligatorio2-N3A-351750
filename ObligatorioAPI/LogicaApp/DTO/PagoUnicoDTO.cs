using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaApp.DTO
{
    public class PagoUnicoDTO : PagoDTO
    {
        [Required]
        public DateTime FechaPago { get; set; }
        [Required]
        public string NumRecibo { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Monto { get; set; }
    }
}