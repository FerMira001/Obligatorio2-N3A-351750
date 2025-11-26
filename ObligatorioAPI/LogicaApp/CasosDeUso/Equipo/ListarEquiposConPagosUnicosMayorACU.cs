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
    public class ListarEquiposConPagosUnicosMayorACU : IListarEquiposConPagosUnicosMayorA
    {
        private IEquipoRepositorio repositorio;

        public ListarEquiposConPagosUnicosMayorACU(IEquipoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public IEnumerable<EquipoDTO> ListarEquiposConPagosUnicosMayorA(decimal monto)
        {
            List<EquipoDTO> equiposDTO = new List<EquipoDTO>();
            foreach (var equipo in repositorio.ObtenerEquiposConPagosUnicosMayorA(monto))
            {
                equiposDTO.Add(EquipoMapper.ToDTO(equipo));
            }
            return equiposDTO;
        }
    }
}