using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.DTO;

namespace LogicaApp.InterfacesCasosDeUso.Equipo
{
    public interface IListarEquiposConPagosUnicosMayorA
    {
        IEnumerable<EquipoDTO> ListarEquiposConPagosUnicosMayorA(decimal monto);
    }
}
