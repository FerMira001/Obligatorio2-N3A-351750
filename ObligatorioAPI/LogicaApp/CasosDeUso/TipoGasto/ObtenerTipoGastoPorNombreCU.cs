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
    public class ObtenerTipoGastoPorNombreCU : IObtenerTipoGastoPorNombre
    {
        public ITipoGastoRepositorio repositorio;
        public ObtenerTipoGastoPorNombreCU(ITipoGastoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }
        TipoGastoDTO IObtenerTipoGastoPorNombre.ObtenerTipoGastoPorNombre(string nombre)
        {
            return Mappers.TipoGastoMapper.ToDTO(repositorio.ObtenerTipoGastoPorNombre(nombre));
        }
    }
}
