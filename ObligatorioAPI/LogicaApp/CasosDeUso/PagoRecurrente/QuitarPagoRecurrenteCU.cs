using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.InterfacesCasosDeUso.PagoRecurrente;
using Estructura.InterfacesRepositorios;

namespace LogicaApp.CasosDeUso.PagoRecurrente
{
    public class QuitarPagoRecurrenteCU : IQuitarPagoRecurrente
    {
        private IPagoRecurrenteRepositorio repositorio;

        public QuitarPagoRecurrenteCU(IPagoRecurrenteRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void QuitarPagoRecurrente(int id)
        {
            repositorio.Quitar(id);
        }
    }
}