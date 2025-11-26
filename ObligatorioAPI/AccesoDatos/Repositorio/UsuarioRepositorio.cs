using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.EntityFramework;
using Estructura.Entidades;
using Estructura.Excepciones;
using Estructura.InterfacesRepositorios;

namespace AccesoDatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        ObligatorioContexto contexto;
        public UsuarioRepositorio(ObligatorioContexto contexto)
        {
            this.contexto = contexto;
        }
        public void Agregar(Usuario nuevo)
        {
            try
            {
                ObtenerUsuarioPorMail(nuevo.mail);
                throw new UsuarioException("Ya existe un usuario con ese mail.");
            }
            catch
            {
                try
                {
                    nuevo.Validar();
                    if (nuevo.equipo != null)
                    {
                        var equipoExistente = contexto.Equipos.FirstOrDefault(e => e.id == nuevo.equipo.id);

                        if (equipoExistente != null)
                            nuevo.equipo = equipoExistente;
                        else
                            throw new UsuarioException("El equipo asignado no existe.");
                    }
                    contexto.Usuarios.Add(nuevo);
                    contexto.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }

        public void Actualizar(Usuario nuevo)
        {
            Usuario userEnBase;
            try
            {
                userEnBase = Encontrar(nuevo.id);
                userEnBase.nombre = nuevo.nombre;
                userEnBase.mail = nuevo.mail;
                userEnBase.apellido = nuevo.apellido;
                userEnBase.password = nuevo.password;
                userEnBase.equipo = nuevo.equipo;
                contexto.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Usuario Encontrar(int id)
        {
            Usuario usuarioBuscado = null;
            usuarioBuscado = contexto.Usuarios.FirstOrDefault(u => u.id == id);
            if (usuarioBuscado == null)
            {
                throw new UsuarioException("No se encontró el usuario con id " + id);
            }
            return usuarioBuscado;
            
        }

        public IEnumerable<Usuario> ObtenerTodos()
        {
            return contexto.Usuarios.ToList();
        }

        public Usuario ObtenerUsuarioPorMail(string mail)
        {
            Usuario usuarioBuscado = null;
            usuarioBuscado = contexto.Usuarios.FirstOrDefault(u => u.mail == mail);
            if (usuarioBuscado == null)
            {
                throw new UsuarioException("No se encontró el usuario con mail " + mail);
            }
            return usuarioBuscado;
        }

        public Usuario ObtenerUsuarioPorNombre(string nombre)
        {
            Usuario usuarioBuscado = null;
            usuarioBuscado = contexto.Usuarios.FirstOrDefault(u => u.nombre == nombre);
            if (usuarioBuscado == null)
            {
                throw new UsuarioException("No se encontró el usuario con nombre " + nombre);
            }
            return usuarioBuscado;
        }

        public void Quitar(int id)
        {
            Usuario usuarioEnBase = null;
            try
            {
                usuarioEnBase = Encontrar(id);
                contexto.Usuarios.Remove(usuarioEnBase);
            }
            catch
            {
                throw;
            }
        }
    }
}
