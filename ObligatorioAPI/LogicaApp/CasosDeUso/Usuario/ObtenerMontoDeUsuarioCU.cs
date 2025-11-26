using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.InterfacesRepositorios;
using LogicaApp.InterfacesCasosDeUso.Usuario;

namespace LogicaApp.CasosDeUso.Usuario
{
    public class ObtenerMontoDeUsuarioCU : IObtenerMontoDeUsuario
    {
        private IPagoRepositorio repositorio;

        public ObtenerMontoDeUsuarioCU(IPagoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        decimal IObtenerMontoDeUsuario.ObtenerMontoDeUsuario(int idUsuario)
        {
            return repositorio.ObtenerMontoDeUsuario(idUsuario);
        }
    }
}
