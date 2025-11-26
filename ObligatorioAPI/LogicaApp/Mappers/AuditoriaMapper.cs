using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Entidades;
using LogicaApp.DTO;

namespace LogicaApp.Mappers
{
    public class AuditoriaMapper
    {
        public static Auditoria FromDTO(AuditoriaDTO dto)
        {
            return new Auditoria
            {
                id = dto.Id,
                idTipoGasto = dto.IdTipoGasto,
                accion = (Auditoria.AccionEnum)dto.Accion,
                fecha = dto.Fecha,
                usuarioId = dto.UsuarioId
            };
        }

        public static AuditoriaDTO ToDTO(Auditoria tipo)
        {
            return new AuditoriaDTO
            {
                Id = tipo.id,
                IdTipoGasto = tipo.idTipoGasto,
                Accion = (AuditoriaDTO.AccionEnum)tipo.accion,
                Fecha = tipo.fecha,
                UsuarioId = tipo.usuarioId
            };
        }
    }
}
