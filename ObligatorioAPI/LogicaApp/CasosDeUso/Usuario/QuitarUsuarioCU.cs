using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.InterfacesCasosDeUso.Usuario;
using Estructura.InterfacesRepositorios;

namespace LogicaApp.CasosDeUso.Usuario
{
    public class QuitarUsuarioCU : IQuitarUsuario
    {
        private IUsuarioRepositorio repositorio;

        public QuitarUsuarioCU(IUsuarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void QuitarUsuario(int id)
        {
            repositorio.Quitar(id);
        }
    }
}