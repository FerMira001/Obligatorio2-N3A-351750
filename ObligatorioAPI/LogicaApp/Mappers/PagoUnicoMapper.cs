using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Entidades;
using LogicaApp.DTO;

namespace LogicaApp.Mappers
{
    public class PagoUnicoMapper
    {
        public static PagoUnico FromDTO(PagoUnicoDTO dto)
        {
            return new PagoUnico
            {
                id = dto.Id,
                metodoPago = (Pago.MetodoPago)dto.MetodoPago,
                tipoGastoId = dto.TipoGastoId,
                usuarioId = dto.UsuarioId,
                desc = dto.Desc,
                fechaPago = dto.FechaPago,
                numRecibo = dto.NumRecibo,
                monto = dto.Monto
            };
        }

        public static PagoUnicoDTO ToDTO(PagoUnico pago)
        {
            return new PagoUnicoDTO
            {
                Id = pago.id,
                MetodoPago = (int)pago.metodoPago,
                TipoGastoId = pago.tipoGastoId,
                UsuarioId = pago.usuarioId,
                Desc = pago.desc,
                FechaPago = pago.fechaPago,
                NumRecibo = pago.numRecibo,
                Monto = pago.monto
            };
        }
    }
}