using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.InterfacesRepositorios;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.Equipo;
using LogicaApp.Mappers;

namespace LogicaApp.CasosDeUso.Equipo
{
    public class ActualiarEquipoCU : IActualizarEquipo
    {
        private IEquipoRepositorio repositorio;

        public ActualiarEquipoCU(IEquipoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void ActualizarEquipo(EquipoDTO modificado)
        {
            repositorio.Actualizar(EquipoMapper.FromDTO(modificado));
        }
    }
}
