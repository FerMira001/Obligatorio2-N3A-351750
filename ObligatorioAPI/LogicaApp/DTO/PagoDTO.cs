using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaApp.DTO
{
    public abstract class PagoDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int MetodoPago { get; set; }
        [Required]
        public int TipoGastoId { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "No se pueden sobrepasar los 200 caracteres.")]
        public string Desc { get; set; }
    }
}
