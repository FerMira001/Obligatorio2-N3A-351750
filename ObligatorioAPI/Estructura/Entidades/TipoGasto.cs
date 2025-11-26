using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Excepciones;
using Estructura.Interfaces;

namespace Estructura.Entidades
{
    public class TipoGasto : IValidable
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string desc { get; set; }

        public void Validar()
        {
            if (id == null)
            {
                throw new TipoGastoException("Error al asignar el ID.");
            }
            if (nombre  == null)
            {
                throw new TipoGastoException("El nombre no puede ser vacío.");
            }
            if (desc == null)
            {
                throw new TipoGastoException("La descripción no puede ser vacía.");
            }
        }
    }
}
