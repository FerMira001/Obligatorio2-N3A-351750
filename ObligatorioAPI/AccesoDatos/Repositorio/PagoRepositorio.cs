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
    public class PagoRepositorio : IPagoRepositorio
    {
        ObligatorioContexto contexto;
        public PagoRepositorio(ObligatorioContexto contexto)
        {
            this.contexto = contexto;
        }

        public void Agregar(Pago nuevo)
        {
            try
            {
                nuevo.Validar();
                contexto.Pagos.Add(nuevo);
                contexto.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void Actualizar(Pago nuevo)
        {
            Pago pagoEnBase = null;
            try
            {
                pagoEnBase = Encontrar(nuevo.id);
                pagoEnBase.tipoGastoId = nuevo.tipoGastoId;
                pagoEnBase.metodoPago = nuevo.metodoPago;
                pagoEnBase.usuarioId = nuevo.usuarioId;
                pagoEnBase.desc = nuevo.desc;
            }
            catch
            {
                throw;
            }
        }

        public Pago Encontrar(int id)
        {
            Pago pagoBuscado = contexto.Pagos.FirstOrDefault(p => p.id == id);
            if (pagoBuscado == null)
            {
                throw new PagoException("No se encontró el pago con id " + id);
            }
            return pagoBuscado;
        }

        public IEnumerable<Pago> ObtenerTodos()
        {
            return contexto.Pagos.ToList();
        }

        public void Quitar(int id)
        {
            Pago pagoAEliminar = null;
            try
            {
                pagoAEliminar = Encontrar(id);
                contexto.Pagos.Remove(pagoAEliminar);
            }
            catch
            {
                throw;
            }
        }

        public decimal ObtenerMontoDeUsuario(int usuarioId)
        {
            decimal montoTotal = 0;
            var pagosUsuario = contexto.Pagos.Where(p => p.usuarioId == usuarioId).ToList();
            foreach (var pago in pagosUsuario)
            {
                if(pago is PagoUnico pagoUnico)
                    montoTotal += pagoUnico.monto;
                else if (pago is PagoRecurrente pagoRecurrente)
                    montoTotal += (pagoRecurrente.montoMensual) * (((pagoRecurrente.fechaFin.Year - pagoRecurrente.fechaInicio.Year) * 12) + pagoRecurrente.fechaFin.Month - pagoRecurrente.fechaInicio.Month);

            }
            return montoTotal;
        }
    }
}
