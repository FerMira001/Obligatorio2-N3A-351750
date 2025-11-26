using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.InterfacesRepositorios;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.PagoUnico;
using LogicaApp.Mappers;

namespace LogicaApp.CasosDeUso.PagoUnico
{
    public class ListarPagosUnicosPorUsuarioCU : IListarPagosUnicosPorUsuario
    {
        private IPagoUnicoRepositorio repositorio;

        public ListarPagosUnicosPorUsuarioCU(IPagoUnicoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public IEnumerable<PagoUnicoDTO> ListarPagosUnicosPorUsuario(int usuarioId)
        {
            List<PagoUnicoDTO> pagosDTO = new List<PagoUnicoDTO>();
            foreach (var pago in repositorio.ObtenerPorUsuario(usuarioId))
            {
                pagosDTO.Add(PagoUnicoMapper.ToDTO(pago));
            }
            return pagosDTO;
        }
    }
}