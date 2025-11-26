using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Excepciones;
using Estructura.Interfaces;

namespace Estructura.Entidades
{
    public class Auditoria : IValidable
    {
        public int id { get; set; }
        public int idTipoGasto { get; set; }
        public AccionEnum accion { get; set; }
        public DateTime fecha { get; set; }
        public int usuarioId { get; set; }


        public enum AccionEnum
        {
            ADICION,
            EDICION,
            ELIMINACION,
        }

        public void Validar()
        {
            if (usuarioId == null || usuarioId < 0)
                throw new AuditoriaException("El usuario no puede ser nulo");
            if (fecha == null)
                throw new AuditoriaException("La fecha no puede ser nula");
            if (idTipoGasto == null || idTipoGasto < 0)
                throw new AuditoriaException("Id de tipo de gasto invalido");
        }
    }
}
