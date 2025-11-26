using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.InterfacesRepositorios;
using LogicaApp.DTO;
using LogicaApp.Mappers;
using LogicaApp.InterfacesCasosDeUso.Usuario;
namespace LogicaApp.CasosDeUso.Usuario
{
    public class LoginUsuarioCU : ILoginUsuario
    {
        private readonly IUsuarioRepositorio _repo;

        public LoginUsuarioCU(IUsuarioRepositorio repo)
        {
            _repo = repo;
        }

        public UsuarioDTO Login(string mail, string password)
        {
            var usuario = _repo.ObtenerUsuarioPorMail(mail);
            if (usuario == null) return null;

            if (usuario.password != password) return null;

            return UsuarioMapper.ToDTO(usuario);
        }
    }
}
