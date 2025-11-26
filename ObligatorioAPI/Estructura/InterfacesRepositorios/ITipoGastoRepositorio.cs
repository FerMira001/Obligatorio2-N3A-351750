using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Entidades;

namespace Estructura.InterfacesRepositorios
{
    public interface ITipoGastoRepositorio : IRepositorio<TipoGasto>
    {
        TipoGasto ObtenerTipoGastoPorNombre(string nombre);
    }
}
