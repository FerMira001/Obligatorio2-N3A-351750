using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.DTO;
using Estructura.InterfacesRepositorios;
using LogicaApp.Mappers;
using LogicaApp.InterfacesCasosDeUso.Equipo;

namespace LogicaApp.CasosDeUso.PagoRecurrente
{
    public class ListarEquiposCU : IListarEquipos
    {
        private IEquipoRepositorio repositorio;

        public ListarEquiposCU(IEquipoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public IEnumerable<EquipoDTO> ListarEquipos()
        {
            List<EquipoDTO> equiposDTO = new List<EquipoDTO>();
            foreach (var equipo in repositorio.ObtenerTodos())
            {
                equiposDTO.Add(EquipoMapper.ToDTO(equipo));
            }
            return equiposDTO;
        }
    }
}