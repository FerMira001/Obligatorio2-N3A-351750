using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.InterfacesRepositorios;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.TipoGasto;
using Estructura.Entidades;
using LogicaApp.Mappers;

namespace LogicaApp.CasosDeUso.TipoGasto
{
    public class ListarTiposGastoCU : IListarTiposDeGasto
    {
        private ITipoGastoRepositorio _repositorio;
        public ListarTiposGastoCU(ITipoGastoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<TipoGastoDTO> ListarTiposDeGasto()
        {
            List<TipoGastoDTO> toReturn = new List<TipoGastoDTO>();
            foreach (Estructura.Entidades.TipoGasto tg in _repositorio.ObtenerTodos())
            {
                toReturn.Add(TipoGastoMapper.ToDTO(tg));
            }
            return toReturn;
        }
    }
}
