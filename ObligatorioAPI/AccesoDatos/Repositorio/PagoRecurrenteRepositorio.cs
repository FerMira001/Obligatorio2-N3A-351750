using System;
using System.Collections.Generic;
using System.Linq;
using AccesoDatos.EntityFramework;
using Estructura.Entidades;
using Estructura.Excepciones;
using Estructura.InterfacesRepositorios;

namespace AccesoDatos.Repositorio
{
    public class PagoRecurrenteRepositorio : IPagoRecurrenteRepositorio
    {
        ObligatorioContexto contexto;
        public PagoRecurrenteRepositorio(ObligatorioContexto contexto)
        {
            this.contexto = contexto;
        }

        public void Agregar(PagoRecurrente nuevo)
        {
            try
            {
                nuevo.Validar();
                contexto.PagosRecurrentes.Add(nuevo);
                contexto.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void Actualizar(PagoRecurrente nuevo)
        {
            PagoRecurrente pagoRecEnBase = null;
            try
            {
                pagoRecEnBase = Encontrar(nuevo.id);
                pagoRecEnBase.tipoGastoId = nuevo.tipoGastoId;
                pagoRecEnBase.metodoPago = nuevo.metodoPago;
                pagoRecEnBase.usuarioId = nuevo.usuarioId;
                pagoRecEnBase.desc = nuevo.desc;
                pagoRecEnBase.fechaInicio = nuevo.fechaInicio;
                pagoRecEnBase.montoMensual = nuevo.montoMensual;
                pagoRecEnBase.fechaFin = nuevo.fechaFin;
            }
            catch
            {
                throw;
            }
        }

        public PagoRecurrente Encontrar(int id)
        {
            var pago = contexto.PagosRecurrentes.FirstOrDefault(p => p.id == id);
            if (pago == null)
                throw new PagoException("No se encontró el pago recurrente con id " + id);
            return pago;
        }

        public IEnumerable<PagoRecurrente> ObtenerTodos()
        {
            return contexto.PagosRecurrentes.ToList();
        }

        public void Quitar(int id)
        {
            PagoRecurrente pagoAEliminar = null;
            try
            {
                pagoAEliminar = Encontrar(id);
                contexto.PagosRecurrentes.Remove(pagoAEliminar);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<PagoRecurrente> ObtenerPorUsuario(int usuarioId)
        {
            return contexto.PagosRecurrentes
                .Where(p => p.usuarioId == usuarioId)
                .ToList();
        }
    }
}
