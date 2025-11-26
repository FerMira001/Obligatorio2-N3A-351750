using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaApp.DTO
{
    public class TipoGastoDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre que proporcionó es inválido.")]
        public string Nombre { get; set; }
        [Required]  
        [StringLength(100, MinimumLength = 3, ErrorMessage = "La descripción no es válida")]
        public string Desc { get; set; }


        public TipoGastoDTO() { }

        public TipoGastoDTO(string nombre, string desc)
        {
            Nombre = nombre;
            Desc = desc;
        }
    }
}
