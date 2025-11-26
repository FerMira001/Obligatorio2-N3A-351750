using Estructura.Entidades;
using LogicaApp.DTO;

public class PagoMapper
{
    public static Pago FromDTO(PagoDTO dto)
    {
        if (dto is PagoRecurrenteDTO recurrenteDto)
        {
            return new PagoRecurrente
            {
                id = recurrenteDto.Id,
                metodoPago = (Pago.MetodoPago)recurrenteDto.MetodoPago,
                tipoGastoId = recurrenteDto.TipoGastoId,
                usuarioId = recurrenteDto.UsuarioId,    
                desc = recurrenteDto.Desc,
                fechaInicio = recurrenteDto.FechaInicio,
                fechaFin = recurrenteDto.FechaFin,
                montoMensual = recurrenteDto.MontoMensual
            };
        }
        else if (dto is PagoUnicoDTO unicoDto)
        {
            return new PagoUnico
            {
                id = unicoDto.Id,
                metodoPago = (Pago.MetodoPago)unicoDto.MetodoPago,
                tipoGastoId = unicoDto.TipoGastoId,
                usuarioId = unicoDto.UsuarioId,    
                desc = unicoDto.Desc,
                fechaPago = unicoDto.FechaPago,
                numRecibo = unicoDto.NumRecibo,
                monto = unicoDto.Monto
            };
        }

        throw new ArgumentException("Tipo de DTO no reconocido");
    }

    public static PagoDTO ToDTO(Pago pago)
    {
        if (pago is PagoRecurrente recurrente)
        {
            return new PagoRecurrenteDTO
            {
                Id = recurrente.id,
                MetodoPago = (int)recurrente.metodoPago,
                TipoGastoId = recurrente.tipoGastoId, 
                UsuarioId = recurrente.usuarioId,     
                Desc = recurrente.desc,
                FechaInicio = recurrente.fechaInicio,
                FechaFin = recurrente.fechaFin,
                MontoMensual = recurrente.montoMensual
            };
        }
        else if (pago is PagoUnico unico)
        {
            return new PagoUnicoDTO
            {
                Id = unico.id,
                MetodoPago = (int)unico.metodoPago,
                TipoGastoId = unico.tipoGastoId,
                UsuarioId = unico.usuarioId,     
                Desc = unico.desc,
                FechaPago = unico.fechaPago,
                NumRecibo = unico.numRecibo,
                Monto = unico.monto
            };
        }

        throw new ArgumentException("Tipo de Pago no reconocido");
    }
}
