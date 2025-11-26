using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.Usuario;
using Estructura.InterfacesRepositorios;
using LogicaApp.Mappers;
using Estructura.Entidades;

namespace LogicaApp.CasosDeUso.Usuario
{
    public class ListarUsuariosCU : IListarUsuarios
    {
        private IUsuarioRepositorio repositorio;

        public ListarUsuariosCU(IUsuarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public IEnumerable<UsuarioDTO> ListarUsuarios()
        {
            List<UsuarioDTO> usuariosDTO = new List<UsuarioDTO>();
            foreach (Estructura.Entidades.Usuario u in repositorio.ObtenerTodos())
            {
                usuariosDTO.Add(UsuarioMapper.ToDTO(u));
            }
            return usuariosDTO;
        }
    }
}