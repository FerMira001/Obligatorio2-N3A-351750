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
    public class ObtenerPagoRecurrenteCU : IObtenerPagoRecurrente
    {
        private IPagoRecurrenteRepositorio repositorio;

        public ObtenerPagoRecurrenteCU(IPagoRecurrenteRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public PagoRecurrenteDTO ObtenerPagoRecurrente(int id)
        {
            return PagoRecurrenteMapper.ToDTO(repositorio.Encontrar(id));
        }
    }
}