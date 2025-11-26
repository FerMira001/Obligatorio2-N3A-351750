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
    public class TiposDeGastoRepositorio : ITipoGastoRepositorio
    {
        private ObligatorioContexto contexto;

        public TiposDeGastoRepositorio(ObligatorioContexto contexto)
        {
            this.contexto = contexto;
        }

        public void Agregar(TipoGasto nuevoTipoGasto)
        {
            TipoGasto nuevo = null;
            try
            {
                nuevo = ObtenerTipoGastoPorNombre(nuevoTipoGasto.nombre);
                if (nuevo != null)
                {
                    throw new TipoGastoException("Ya existe un tipo de gasto con ese nombre.");
                }
            }
            catch (TipoGastoException ex)
            {
                try
                {
                    nuevoTipoGasto.Validar();
                    contexto.TiposDeGasto.Add(nuevoTipoGasto);
                    contexto.SaveChanges();

                }
                catch (TipoGastoException e)
                {
                    throw e;
                }
            }
            catch(Exception ex)
            {
                throw new TipoGastoException("Error al agregar el tipo de gasto. " + ex.Message);
            }
        }

        public void Actualizar(TipoGasto nuevo)
        {
            TipoGasto tipoEnBase;
            try
            {
                nuevo.Validar();
                tipoEnBase = Encontrar(nuevo.id);
            }
            catch { throw; }

            if (tipoEnBase != null)
            {
                tipoEnBase.nombre = nuevo.nombre;
                tipoEnBase.desc = nuevo.desc;
                contexto.SaveChanges();
            }
            else
            {
                throw new TipoGastoException("No se encontró el tipo de gasto a actualizar.");
            }
        }

        public TipoGasto Encontrar(int id)
        {
            var tipoBuscado = contexto.TiposDeGasto.FirstOrDefault(t => t.id == id);
            if (tipoBuscado == null)
            {
                throw new TipoGastoException("No se encontró el tipo de gasto con el ID proporcionado.");
            }
            return tipoBuscado;
        }

        public IEnumerable<TipoGasto> ObtenerTodos()
        {
            try
            {
                return contexto.TiposDeGasto.ToList();
            }
            catch (Exception ex)
            {
                throw new TipoGastoException("Error al obtener todos los tipos de gasto. " + ex.Message);
            }
        }

        public TipoGasto ObtenerTipoGastoPorNombre(string nombre)
        {
            var tipoBuscado = contexto.TiposDeGasto.FirstOrDefault(t => t.nombre == nombre);
            if (tipoBuscado == null)
            {
                throw new TipoGastoException("No se encontró el tipo de gasto con el nombre proporcionado.");
            }
            return tipoBuscado;
        }

        public void Quitar(int id)
        {
            var tipoAEliminar = Encontrar(id);
            if (tipoAEliminar != null)
            {
                contexto.TiposDeGasto.Remove(tipoAEliminar);
                contexto.SaveChanges();
            }
            else
            {
                throw new TipoGastoException("No se encontró el tipo de gasto a eliminar.");
            }
        }
    }
}
