using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.DTO;

namespace LogicaApp.InterfacesCasosDeUso.PagoRecurrente
{
    public interface IListarPagosRecMes
    {
        IEnumerable<PagoRecurrenteDTO> ListarPagosRecMes(int mes, int anio);
    }
}
