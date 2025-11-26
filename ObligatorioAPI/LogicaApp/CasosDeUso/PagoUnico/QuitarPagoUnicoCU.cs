using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.InterfacesCasosDeUso.PagoUnico;
using Estructura.InterfacesRepositorios;

namespace LogicaApp.CasosDeUso.PagoUnico
{
    public class QuitarPagoUnicoCU : IQuitarPagoUnico
    {
        private IPagoUnicoRepositorio repositorio;

        public QuitarPagoUnicoCU(IPagoUnicoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void QuitarPagoUnico(int id)
        {
            repositorio.Quitar(id);
        }
    }
}