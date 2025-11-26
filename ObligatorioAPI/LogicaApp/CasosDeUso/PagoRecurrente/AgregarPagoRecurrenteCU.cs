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
    public class AgregarPagoRecurrenteCU : IAgregarPagoRecurrente
    {
        private IPagoRecurrenteRepositorio repositorio;

        public AgregarPagoRecurrenteCU(IPagoRecurrenteRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void AgregarPagoRecurrente(PagoRecurrenteDTO nuevo)
        {
            repositorio.Agregar(PagoRecurrenteMapper.FromDTO(nuevo));
        }
    }
}