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
    public class AgregarUsuarioCU : IAgregarUsuario
    {
        private IUsuarioRepositorio repositorio;

        public AgregarUsuarioCU(IUsuarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void AgregarUsuario(UsuarioDTO nuevo)
        {
            repositorio.Agregar(UsuarioMapper.FromDTO(nuevo));
        }
    }
}