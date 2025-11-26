using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.InterfacesRepositorios;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.TipoGasto;

namespace LogicaApp.CasosDeUso.TipoGasto
{
    public class ObtenerTipoGastoCU : IObtenerTipoGasto
    {
        public ITipoGastoRepositorio repositorio;
        public ObtenerTipoGastoCU(ITipoGastoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        TipoGastoDTO IObtenerTipoGasto.ObtenerTipoGasto(int id)
        {
            return Mappers.TipoGastoMapper.ToDTO(repositorio.Encontrar(id));
        }
    }
}
