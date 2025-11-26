using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.PagoRecurrente;
using Estructura.InterfacesRepositorios;
using LogicaApp.Mappers;

namespace LogicaApp.CasosDeUso.PagoRecurrente
{
    public class ListarPagosRecurrentesCU : IListarPagosRecurrentes
    {
        private IPagoRecurrenteRepositorio repositorio;

        public ListarPagosRecurrentesCU(IPagoRecurrenteRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public IEnumerable<PagoRecurrenteDTO> ListarPagosRecurrentes()
        {
            List<PagoRecurrenteDTO> pagosDTO = new List<PagoRecurrenteDTO>();
            foreach (var pago in repositorio.ObtenerTodos())
            {
                pagosDTO.Add(PagoRecurrenteMapper.ToDTO(pago));
            }
            return pagosDTO;
        }            
    }
}