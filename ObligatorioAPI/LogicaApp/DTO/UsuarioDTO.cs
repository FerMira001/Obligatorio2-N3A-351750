using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaApp.DTO
{
    public class UsuarioDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nombre inválido")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Apellido inválido")]
        public string Apellido { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Formato del correo inválido")]
        public string Mail { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Contraseña inválida")]
        public string Password { get; set; }
        [Required]
        public EquipoDTO Equipo { get; set; }
        [Required]
        public int Rol { get; set; }
        public string Token { get; set; }


        public UsuarioDTO()
        {
        }

        public UsuarioDTO(string nombre, string apellido, string mail, string password, EquipoDTO equipo, int rol)
        {
            Nombre = nombre;
            Apellido = apellido;
            Mail = mail;
            Password = password;
            Equipo = equipo;
            Rol = rol;
        }
    }

}