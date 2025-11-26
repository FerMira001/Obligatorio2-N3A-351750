using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Entidades;
using LogicaApp.DTO;

namespace LogicaApp.Mappers
{
    public class TipoGastoMapper
    {
        public static TipoGasto FromDTO (TipoGastoDTO dto)
        {
            return new TipoGasto
            {
                id = dto.Id,
                nombre = dto.Nombre,
                desc = dto.Desc,
            };
        }

        public static TipoGastoDTO ToDTO(TipoGasto tipo)
        {
            return new TipoGastoDTO
            {
                Id = tipo.id,
                Nombre = tipo.nombre,
                Desc = tipo.desc,
            };
        }
    }
}
