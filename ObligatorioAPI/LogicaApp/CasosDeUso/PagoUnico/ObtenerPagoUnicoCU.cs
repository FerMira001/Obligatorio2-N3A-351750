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
    public class ObtenerPagoUnicoCU : IObtenerPagoUnico
    {
        private IPagoUnicoRepositorio repositorio;

        public ObtenerPagoUnicoCU(IPagoUnicoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public PagoUnicoDTO ObtenerPagoUnico(int id)
        {
            return PagoUnicoMapper.ToDTO(repositorio.Encontrar(id));
        }
    }
}