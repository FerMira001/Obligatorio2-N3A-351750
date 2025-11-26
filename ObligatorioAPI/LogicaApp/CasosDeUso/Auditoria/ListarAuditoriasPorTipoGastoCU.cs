using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.InterfacesRepositorios;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.Auditoria;
using LogicaApp.Mappers;

namespace LogicaApp.CasosDeUso.Auditoria
{
    public class ListarAuditoriasPorTipoGastoCU : IListarAuditoriasPorTipoGasto
    {
        private IAuditoriaRepositorio repositorio;

        public ListarAuditoriasPorTipoGastoCU(IAuditoriaRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public IEnumerable<AuditoriaDTO> ListarAuditoriasPorTipoGasto(int tipoGastoId)
        {
            List<AuditoriaDTO> auditoriasDTO = new List<AuditoriaDTO>();
            foreach (var auditoria in repositorio.ObtenerPorTipoGasto(tipoGastoId))
            {
                auditoriasDTO.Add(AuditoriaMapper.ToDTO(auditoria));
            }
            return auditoriasDTO;
        }
    }
}