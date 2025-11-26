using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.PagoUnico;
using Estructura.InterfacesRepositorios;
using LogicaApp.Mappers;

namespace LogicaApp.CasosDeUso.PagoUnico
{
    public class ListarPagosUnicosCU : IListarPagosUnicos
    {
        private IPagoUnicoRepositorio repositorio;

        public ListarPagosUnicosCU(IPagoUnicoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public IEnumerable<PagoUnicoDTO> ListarPagosUnicos()
        {
            List<PagoUnicoDTO> pagosDTO = new List<PagoUnicoDTO>();
            foreach (var pago in repositorio.ObtenerTodos())
            {
                pagosDTO.Add(PagoUnicoMapper.ToDTO(pago));
            }
            return pagosDTO;
        }
    }
}