using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.EntityFramework;
using Estructura.Entidades;
using Estructura.Excepciones;
using Estructura.InterfacesRepositorios;

namespace AccesoDatos.Repositorio
{
    public class AuditoriaRepositorio : IAuditoriaRepositorio
    {
        private ObligatorioContexto contexto;

        public AuditoriaRepositorio(ObligatorioContexto contexto)
        {
            this.contexto = contexto;
        }


        public void Actualizar(Auditoria nuevo)
        {
            throw new AuditoriaException("NO SE PUEDE EDITAR UN REGISTRO DE AUDITORIA");
        }

        public void Agregar(Auditoria nuevo)
        {
            try
            {
                nuevo.Validar();
                contexto.Auditorias.Add(nuevo);
                contexto.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Auditoria Encontrar(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Auditoria> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public void Quitar(int id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Auditoria> ObtenerPorTipoGasto(int tipoGastoId)
        {
            return contexto.Auditorias
                .Where(a => a.idTipoGasto == tipoGastoId)
                .ToList();
        }
    }
}
