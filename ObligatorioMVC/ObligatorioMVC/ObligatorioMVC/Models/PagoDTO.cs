using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioMVC.Models
{
    public abstract class PagoDTO
    {
        public int Id { get; set; }
        public int MetodoPago { get; set; }
        public int TipoGastoId { get; set; }
        public int UsuarioId { get; set; }
        public string Desc { get; set; }
    }
}
