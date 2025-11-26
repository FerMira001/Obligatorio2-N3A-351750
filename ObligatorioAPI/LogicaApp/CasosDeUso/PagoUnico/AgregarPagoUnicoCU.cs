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
    public class AgregarPagoUnicoCU : IAgregarPagoUnico
    {
        private IPagoUnicoRepositorio repositorio;

        public AgregarPagoUnicoCU(IPagoUnicoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void AgregarPagoUnico(PagoUnicoDTO nuevo)
        {
            repositorio.Agregar(PagoUnicoMapper.FromDTO(nuevo));
        }
    }
}