using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Excepciones
{
    public class TipoGastoException : Exception
    {
        public TipoGastoException() { }
        public TipoGastoException(string mensaje)
        : base(mensaje) { }
        public TipoGastoException(string mensaje, Exception ex)
        : base(mensaje, ex) { }
    }
}
