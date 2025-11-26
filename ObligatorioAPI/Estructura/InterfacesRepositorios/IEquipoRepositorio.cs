using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Entidades;

namespace Estructura.InterfacesRepositorios
{
    public interface IEquipoRepositorio:IRepositorio<Entidades.Equipo>
    {
        Equipo ObtenerEquipoPorNombre(string nombre);
        IEnumerable<Equipo> ObtenerEquiposConPagosUnicosMayorA(decimal monto);
    }
}
