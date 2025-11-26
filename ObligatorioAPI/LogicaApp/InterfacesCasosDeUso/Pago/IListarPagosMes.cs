using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.DTO;

namespace LogicaApp.InterfacesCasosDeUso.Pago
{
    public interface IListarPagosMes
    {
        IEnumerable<PagoDTO> ListarPagosMes(int mes, int anio);
    }
}
