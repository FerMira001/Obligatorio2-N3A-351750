using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Interfaces;

namespace Estructura.Entidades
{
    public class Equipo : IValidable
    {
        public int id { get; set; }
        public string nombre { get; set; }

        public void Validar()
        {
            if (nombre == null)
            {
                throw new Exception("El nombre no puede ser vacío.");
            }
        }
    }
}
