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
    public class ActualizarPagoRecurrenteCU : IActualizarPagoRecurrente
    {
        private IPagoRecurrenteRepositorio repositorio;

        public ActualizarPagoRecurrenteCU(IPagoRecurrenteRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void ActualizarPagoRecurrente(PagoRecurrenteDTO modificado)
        {
            repositorio.Actualizar(PagoRecurrenteMapper.FromDTO(modificado));
        }
    }
}