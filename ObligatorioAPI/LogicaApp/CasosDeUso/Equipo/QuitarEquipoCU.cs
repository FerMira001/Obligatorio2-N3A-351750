using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.InterfacesCasosDeUso.Equipo;
using Estructura.InterfacesRepositorios;

namespace LogicaApp.CasosDeUso.Equipo
{
    public class QuitarEquipoCU : IQuitarEquipo
    {
        private IEquipoRepositorio repositorio;

        public QuitarEquipoCU(IEquipoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void QuitarEquipo(int id)
        {
            repositorio.Quitar(id);
        }
    }
}