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
    public class ObtenerUsuarioCU : IObtenerUsuario
    {
        private IUsuarioRepositorio repositorio;

        public ObtenerUsuarioCU(IUsuarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public UsuarioDTO ObtenerUsuario(int id)
        {
            return UsuarioMapper.ToDTO(repositorio.Encontrar(id));
        }
    }
}