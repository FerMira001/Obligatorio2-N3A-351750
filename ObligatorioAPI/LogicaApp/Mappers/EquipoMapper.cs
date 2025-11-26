using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Entidades;
using LogicaApp.DTO;

namespace LogicaApp.Mappers
{
    public class EquipoMapper
    {
        public static Equipo FromDTO(EquipoDTO dto)
        {
            return new Equipo
            {
                id = dto.Id,
                nombre = dto.Nombre
            };
        }

        public static EquipoDTO ToDTO(Equipo tipo)
        {
            return new EquipoDTO
            {
                Id = tipo.id,
                Nombre = tipo.nombre
            };
        }
    }
}
