using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.InterfacesRepositorios;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.PagoRecurrente;
using LogicaApp.Mappers;

namespace LogicaApp.CasosDeUso.PagoRecurrente
{
    public class ListarPagosRecurrentesPorUsuarioCU : IListarPagosRecurrentesPorUsuario
    {
        private IPagoRecurrenteRepositorio repositorio;

        public ListarPagosRecurrentesPorUsuarioCU(IPagoRecurrenteRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public IEnumerable<PagoRecurrenteDTO> ListarPagosRecurrentesPorUsuario(int usuarioId)
        {
            List<PagoRecurrenteDTO> pagosDTO = new List<PagoRecurrenteDTO>();
            foreach (var pago in repositorio.ObtenerPorUsuario(usuarioId))
            {
                pagosDTO.Add(PagoRecurrenteMapper.ToDTO(pago));
            }
            return pagosDTO;
        }
    }
}