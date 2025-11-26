using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.InterfacesRepositorios;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.Pago;
using LogicaApp.CasosDeUso.PagoUnico;
using LogicaApp.Mappers;
using LogicaApp.InterfacesCasosDeUso.PagoRecurrente;
using LogicaApp.InterfacesCasosDeUso.PagoUnico;

namespace LogicaApp.CasosDeUso.PagoRecurrente
{
    public class ListarPagosRecMesCU : IListarPagosRecMes
    {
        private IListarPagosRecurrentes listarPagosRecurrentes;

        public ListarPagosRecMesCU(IListarPagosRecurrentes listarPagosRecurrentes)
        {
            this.listarPagosRecurrentes = listarPagosRecurrentes;
        }
        
        IEnumerable<PagoRecurrenteDTO> IListarPagosRecMes.ListarPagosRecMes(int mes, int anio)
        {
            List<PagoRecurrenteDTO> pagosDelMes = new List<PagoRecurrenteDTO>();
            foreach (var pago in listarPagosRecurrentes.ListarPagosRecurrentes())
            {
                if (pago.FechaInicio.Month <= mes && pago.FechaInicio.Year <= anio)
                {
                    if (pago.FechaFin == null || pago.FechaFin.Month >= mes && pago.FechaFin.Year >= anio)
                    {
                        pagosDelMes.Add(pago);
                        
                    }
                }
            }
            return pagosDelMes;
        }
    }
}
