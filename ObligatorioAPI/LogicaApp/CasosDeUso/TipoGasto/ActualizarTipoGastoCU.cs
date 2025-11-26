using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.InterfacesRepositorios;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.TipoGasto;
using LogicaApp.Mappers;

namespace LogicaApp.CasosDeUso.TipoGasto
{
    public class ActualizarTipoGastoCU : IActualizarTipoGasto
    {
        private ITipoGastoRepositorio repositorio;

        public ActualizarTipoGastoCU(ITipoGastoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }
        public void ActualizarTipoGasto(TipoGastoDTO modificado)
        {
            repositorio.Actualizar(TipoGastoMapper.FromDTO(modificado));
        }
    }
}
