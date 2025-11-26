using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaApp.DTO
{
    public class PagoRecurrenteDTO : PagoDTO
    {
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal MontoMensual { get; set; }
    }
}