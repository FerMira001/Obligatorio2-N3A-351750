using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.InterfacesCasosDeUso.TipoGasto;
using Estructura.InterfacesRepositorios;

namespace LogicaApp.CasosDeUso.TipoGasto
{
    public class QuitarTipoGastoCU : IQuitarTipoGasto
    {
        private ITipoGastoRepositorio repositorio;

        public QuitarTipoGastoCU(ITipoGastoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void QuitarTipoGasto(int id)
        {
            repositorio.Quitar(id);
        }
    }
}