using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Entidades;
using LogicaApp.DTO;

namespace LogicaApp.Mappers
{
    public class PagoRecurrenteMapper
    {
        public static PagoRecurrente FromDTO(PagoRecurrenteDTO dto)
        {
            return new PagoRecurrente
            {
                id = dto.Id,
                metodoPago = (Pago.MetodoPago)dto.MetodoPago,
                tipoGastoId = dto.TipoGastoId,
                usuarioId = dto.UsuarioId,
                desc = dto.Desc,
                fechaInicio = dto.FechaInicio,
                fechaFin = dto.FechaFin,
                montoMensual = dto.MontoMensual
            };
        }

        public static PagoRecurrenteDTO ToDTO(PagoRecurrente pago)
        {
            return new PagoRecurrenteDTO
            {
                Id = pago.id,
                MetodoPago = (int)pago.metodoPago,
                TipoGastoId = pago.tipoGastoId,
                UsuarioId = pago.usuarioId,
                Desc = pago.desc,
                FechaInicio = pago.fechaInicio,
                FechaFin = pago.fechaFin,
                MontoMensual = pago.montoMensual
            };
        }
    }
}