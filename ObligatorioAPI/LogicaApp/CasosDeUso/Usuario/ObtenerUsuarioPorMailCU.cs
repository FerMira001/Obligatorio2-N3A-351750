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
    public class ObtenerUsuarioPorMailCU : IObtenerUsuarioPorMail
    {
        private IUsuarioRepositorio repositorio;

        public ObtenerUsuarioPorMailCU(IUsuarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public UsuarioDTO ObtenerUsuarioPorMail(string mail)
        {
            mail = mail.ToLower();
            var usuario = repositorio.ObtenerTodos().FirstOrDefault(u => u.mail.ToLower() == mail);
            return UsuarioMapper.ToDTO(usuario);
        }
    }
}