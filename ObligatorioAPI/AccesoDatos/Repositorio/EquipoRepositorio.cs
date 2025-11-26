using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.EntityFramework;
using Estructura.Entidades;
using Estructura.Excepciones;
using Estructura.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Repositorio
{
    public class EquipoRepositorio : IEquipoRepositorio
    {
        private ObligatorioContexto contexto;

        public EquipoRepositorio(ObligatorioContexto contexto)
        {
            this.contexto = contexto;
        }

        public void Agregar(Equipo nuevo)
        {
            Equipo equipo = null;
            try
            {
                equipo = ObtenerEquipoPorNombre(nuevo.nombre);
            }
            catch { throw; }

            if (equipo != null)
            {
                throw new EquipoException("Ya existe un equipo con ese nombre.");
            }
            try
            {
                nuevo.Validar();
                contexto.Equipos.Add(nuevo);
                contexto.SaveChanges();
            }
            catch { throw; }
        }

        public void Actualizar(Equipo nuevo)
        {
            Equipo equipoEnBase;
            try
            {
                nuevo.Validar();
                equipoEnBase = Encontrar(nuevo.id);
            }
            catch { throw; }
            if (equipoEnBase != null)
            {
                equipoEnBase.nombre = nuevo.nombre;
                try
                {
                    contexto.SaveChanges();
                }
                catch { throw; }
            }
            else
            {
                throw new EquipoException("No se encontró el equipo con id " + nuevo.id);
            }
        }

        public Equipo Encontrar(int id)
        {
            Equipo equipo = null;
            try
            {
                equipo = contexto.Equipos.FirstOrDefault(e => e.id == id);
            }
            catch { throw; }
            if (equipo == null)
            {
                throw new EquipoException("No se encontró el equipo con id " + id);
            }
            return equipo;
        }

        public IEnumerable<Equipo> ObtenerTodos()
        {
            try
            {
                return contexto.Equipos.ToList();
            }
            catch(Exception ex)
            {
                throw new EquipoException("Error al listar los equipos." + ex);
            }
        }

        public Equipo ObtenerEquipoPorNombre(string nombre)
        {
            Equipo equipo = null;
            try
            {
                equipo = contexto.Equipos.FirstOrDefault(e => e.nombre == nombre);
            }
            catch { throw; }
            if (equipo == null)
            {
                throw new EquipoException("No se encontró el equipo con nombre " + nombre);
            }
            return equipo;
        }

        public void Quitar(int id)
        {
            Equipo equipo;
            try
            {
                equipo = Encontrar(id);
            }
            catch { throw; }
            if (equipo != null)
            {
                try
                {
                    contexto.Equipos.Remove(equipo);
                    contexto.SaveChanges();
                }
                catch
                {
                    throw new EquipoException("No se pudo eliminar el equipo con id " + id);
                }
            }
            else
            {
                throw new EquipoException("No se encontró el equipo con id " + id);
            }
        }

        public IEnumerable<Equipo> ObtenerEquiposConPagosUnicosMayorA(decimal monto)
        {
            return contexto.PagosUnicos
                .Where(p => p.monto > monto)
                .Join(contexto.Usuarios,
                    pago => pago.usuarioId,
                    usuario => usuario.id,
                    (pago, usuario) => usuario.equipo)
                .Where(equipo => equipo != null)
                .Distinct()
                .OrderByDescending(e => e.nombre)
                .ToList();
        }
    }
}
