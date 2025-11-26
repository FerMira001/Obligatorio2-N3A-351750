using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.TipoGasto;
using Estructura.InterfacesRepositorios;
using LogicaApp.Mappers;

namespace LogicaApp.CasosDeUso.TipoGasto
{
    public class AgregarTipoGastoCU : IAgregarTipoGasto
    {
        private ITipoGastoRepositorio repositorio;
        public AgregarTipoGastoCU(ITipoGastoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }
        public void AgregarTipoGasto(TipoGastoDTO nuevo)
        {
            repositorio.Agregar(TipoGastoMapper.FromDTO(nuevo));
        }
    }
}
