using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.Usuario;
using Estructura.InterfacesRepositorios;
using LogicaApp.Mappers;

namespace LogicaApp.CasosDeUso.Usuario
{
    public class ActualizarUsuarioCU : IActualizarUsuario
    {
        private IUsuarioRepositorio repositorio;

        public ActualizarUsuarioCU(IUsuarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void ActualizarUsuario(UsuarioDTO modificado)
        {
            repositorio.Actualizar(UsuarioMapper.FromDTO(modificado));
        }
    }
}