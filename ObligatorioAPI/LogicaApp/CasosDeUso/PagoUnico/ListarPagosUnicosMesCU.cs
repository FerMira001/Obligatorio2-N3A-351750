using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.InterfacesRepositorios;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.Pago;
using LogicaApp.CasosDeUso.PagoRecurrente;
using LogicaApp.CasosDeUso.PagoUnico;
using LogicaApp.Mappers;
using LogicaApp.InterfacesCasosDeUso.PagoRecurrente;
using LogicaApp.InterfacesCasosDeUso.PagoUnico;

namespace LogicaApp.CasosDeUso.Pago
{
    public class ListarPagosUnicosMesCU : IListarPagosUnicosMes
    {
        private IListarPagosUnicos listarPagosUnicos;

        public ListarPagosUnicosMesCU (IListarPagosUnicos listarPagosUnicos)
        {
            this.listarPagosUnicos = listarPagosUnicos;
        }

        public IEnumerable<PagoUnicoDTO> ListarPagosUnicosMes(int mes, int anio)
        {
            List<PagoUnicoDTO> pagosDelMes = new List<PagoUnicoDTO>();
            foreach (var pago in listarPagosUnicos.ListarPagosUnicos())
            {
                if (pago.FechaPago.Month == mes && pago.FechaPago.Year == anio)
                {
                    pagosDelMes.Add(pago);
                }
            }
            return pagosDelMes;
        }
    }
}
