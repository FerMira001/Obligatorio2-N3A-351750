using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.InterfacesRepositorios;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.Pago;
using LogicaApp.Mappers;

namespace LogicaApp.CasosDeUso.Pago
{
    public class TipoGastoEnUsoCU : ITipoGastoEnUso
    {
        public IPagoRepositorio repositorio;

        public TipoGastoEnUsoCU(IPagoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public bool TipoGastoEnUso(int tipoGastoId)
        {
            return repositorio.ObtenerTodos().FirstOrDefault(p => p.tipoGastoId == tipoGastoId) != null;
        }
    }
}
