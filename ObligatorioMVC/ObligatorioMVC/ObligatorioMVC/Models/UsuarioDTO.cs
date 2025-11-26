using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioMVC.Models
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public EquipoDTO Equipo { get; set; }
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