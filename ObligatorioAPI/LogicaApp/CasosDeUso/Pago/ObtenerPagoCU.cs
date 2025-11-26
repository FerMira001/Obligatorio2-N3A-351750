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
    public class ObtenerPagoCU : IObtenerPago
    {
        private IPagoRepositorio repositorio;

        public ObtenerPagoCU(IPagoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public PagoDTO ObtenerPago(int id)
        {
            return PagoMapper.ToDTO(repositorio.Encontrar(id));
        }
    }
}
