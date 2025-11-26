using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.Equipo;
using Estructura.InterfacesRepositorios;
using LogicaApp.Mappers;

namespace LogicaApp.CasosDeUso.Equipo
{
    public class ModificarEquipoCU : IActualizarEquipo
    {
        private IEquipoRepositorio repositorio;

        public ModificarEquipoCU(IEquipoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void ActualizarEquipo(EquipoDTO modificado)
        {
            repositorio.Actualizar(EquipoMapper.FromDTO(modificado));
        }
    }
}