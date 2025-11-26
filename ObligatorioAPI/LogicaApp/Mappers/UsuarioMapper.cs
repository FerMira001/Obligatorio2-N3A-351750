using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Entidades;
using LogicaApp.DTO;

namespace LogicaApp.Mappers
{
    public class UsuarioMapper
    {
        public static Usuario FromDTO(UsuarioDTO dto)
        {
            return new Usuario
            {
                id = dto.Id,
                nombre = dto.Nombre,
                apellido = dto.Apellido,
                mail = dto.Mail,
                password = dto.Password,
                equipo = dto.Equipo != null ? EquipoMapper.FromDTO(dto.Equipo) : null,
                rol = (Usuario.Rol)dto.Rol
            };
        }

        public static UsuarioDTO ToDTO(Usuario usuario)
        {
            if (usuario == null) return null;
            return new UsuarioDTO
            {
                Id = usuario.id,
                Nombre = usuario.nombre,
                Apellido = usuario.apellido,
                Mail = usuario.mail,
                Password = usuario.password,
                Equipo = usuario.equipo != null ? EquipoMapper.ToDTO(usuario.equipo) : null,
                Rol = (int)usuario.rol
            };
        }
    }
}