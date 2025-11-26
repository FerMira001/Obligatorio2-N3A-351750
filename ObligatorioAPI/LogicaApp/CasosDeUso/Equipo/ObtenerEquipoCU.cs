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
    public class ObtenerEquipoCU : IObtenerEquipo
    {
        private IEquipoRepositorio repositorio;

        public ObtenerEquipoCU(IEquipoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public EquipoDTO ObtenerEquipo(int id)
        {
            return EquipoMapper.ToDTO(repositorio.Encontrar(id));
        }
    }
}
