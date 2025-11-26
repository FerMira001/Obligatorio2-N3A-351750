using System;
using System.Collections.Generic;
using System.Linq;
using AccesoDatos.EntityFramework;
using Estructura.Entidades;
using Estructura.Excepciones;
using Estructura.InterfacesRepositorios;

namespace AccesoDatos.Repositorio
{
    public class PagoUnicoRepositorio : IPagoUnicoRepositorio
    {
        ObligatorioContexto contexto;
        public PagoUnicoRepositorio(ObligatorioContexto contexto)
        {
            this.contexto = contexto;
        }
        public void Agregar(PagoUnico nuevo)
        {
            try
            {
                nuevo.Validar();
                contexto.PagosUnicos.Add(nuevo);
                contexto.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void Actualizar(PagoUnico nuevo)
        {
            PagoUnico pagoUnicoEnBase = null;
            try
            {
                pagoUnicoEnBase = Encontrar(nuevo.id);
                pagoUnicoEnBase.tipoGastoId = nuevo.tipoGastoId;
                pagoUnicoEnBase.metodoPago = nuevo.metodoPago;
                pagoUnicoEnBase.usuarioId = nuevo.usuarioId;
                pagoUnicoEnBase.desc = nuevo.desc;
                pagoUnicoEnBase.numRecibo = nuevo.numRecibo;
                pagoUnicoEnBase.fechaPago = nuevo.fechaPago;
                pagoUnicoEnBase.monto = nuevo.monto;
            }
            catch
            {
                throw;
            }
        }

        public PagoUnico Encontrar(int id)
        {
            var pago = contexto.PagosUnicos.FirstOrDefault(p => p.id == id);
            if (pago == null)
                throw new PagoException("No se encontró el pago único con id " + id);
            return pago;
        }

        public IEnumerable<PagoUnico> ObtenerTodos()
        {
            return contexto.PagosUnicos.ToList();
        }

        public void Quitar(int id)
        {
            PagoUnico pagoAEliminar = null;
            try
            {
                pagoAEliminar = Encontrar(id);
                contexto.PagosUnicos.Remove(pagoAEliminar);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<PagoUnico> ObtenerPorUsuario(int usuarioId)
        {
            return contexto.PagosUnicos
                .Where(p => p.usuarioId == usuarioId)
                .ToList();
        }
    }
}
