using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Entidades;

namespace Estructura.InterfacesRepositorios
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Usuario ObtenerUsuarioPorNombre(string nombre);
        Usuario ObtenerUsuarioPorMail(string mail);
    }
}
