using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Entidades;

namespace LogicaApp.DTO
{
    public class AuditoriaDTO
    {
        public int Id { get; set; }
        public int IdTipoGasto { get; set; }
        public AccionEnum Accion { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }


        public enum AccionEnum
        {
            ADICION,
            EDICION,
            ELIMINACION,
        }

        public AuditoriaDTO() { }

        public AuditoriaDTO(int idTipoGasto, AccionEnum accion, int usuarioId)
        {
            IdTipoGasto = idTipoGasto;
            Accion = accion;
            Fecha = DateTime.Now;
            UsuarioId = usuarioId;
        }
    }
}
