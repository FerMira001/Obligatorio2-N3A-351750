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
    public class ActualizarPagoUnicoCU : IActualizarPagoUnico
    {
        private IPagoUnicoRepositorio repositorio;

        public ActualizarPagoUnicoCU(IPagoUnicoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void ActualizarPagoUnico(PagoUnicoDTO modificado)
        {
            repositorio.Actualizar(PagoUnicoMapper.FromDTO(modificado));
        }
    }
}