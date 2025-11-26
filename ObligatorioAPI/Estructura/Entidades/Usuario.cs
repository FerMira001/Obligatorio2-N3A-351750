using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Excepciones;
using Estructura.Interfaces;

namespace Estructura.Entidades
{
    public class Usuario : IValidable
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public Equipo? equipo { get; set; }
        public enum Rol
        {
            Administrador,
            Gerente,
            Empleado
        }
        public Rol rol { get; set; }

        public void Validar()
        {
            if (nombre == null || nombre.Trim() == "")
            {
                throw new UsuarioException("El nombre no puede ser vacío.");
            }
            if (apellido == null || apellido.Trim() == "")
            {
                throw new UsuarioException("El apellido no puede ser vacío.");
            }
            if (mail == null || mail.Trim() == "")
            {
                throw new UsuarioException("El mail no puede ser vacío.");
            }
            if (password == null || password.Trim() == "")
            {
                throw new UsuarioException("La contraseña no puede ser vacía.");
            }
            if(password.Length < 8)
            {
                throw new UsuarioException("La contraseña debe tener al menos 8 caracteres.");
            }
        }
    }
}
