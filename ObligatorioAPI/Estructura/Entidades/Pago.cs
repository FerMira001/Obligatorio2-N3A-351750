using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Excepciones;
using Estructura.Interfaces;

namespace Estructura.Entidades
{
    public abstract class Pago : IValidable
    {
        public int id { get; set; }
        public MetodoPago metodoPago { get; set; }
        public int tipoGastoId { get; set; }
        public int usuarioId { get; set; }
        public string desc { get; set; }

        public enum MetodoPago
        {
            Credito,
            Efectivo
        }

        public void Validar()
        {
            if (metodoPago == null)
            {
                throw new PagoException("El método de pago no puede ser nulo.");
            }
            if (tipoGastoId == null)
            {
                throw new PagoException("El tipo de gasto no puede ser nulo.");
            }
            if (usuarioId == null)
            {
                throw new PagoException("El usuario no puede ser nulo.");
            }
            if (desc == null || desc.Trim() == "")
            {
                throw new PagoException("La descripción no puede ser nula.");
            }
        }
    }
}
