using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.DTO;

namespace LogicaApp.InterfacesCasosDeUso.PagoUnico
{
    public interface IListarPagosUnicos
    {
        IEnumerable<PagoUnicoDTO> ListarPagosUnicos();
    }
}