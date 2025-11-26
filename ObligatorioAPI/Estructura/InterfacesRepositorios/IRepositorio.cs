using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.InterfacesRepositorios
{
    public interface IRepositorio<T>
    {
        IEnumerable<T> ObtenerTodos();
        T Encontrar(int id);
        void Agregar(T nuevo);
        void Quitar(int id);
        void Actualizar(T nuevo);
    }
}
