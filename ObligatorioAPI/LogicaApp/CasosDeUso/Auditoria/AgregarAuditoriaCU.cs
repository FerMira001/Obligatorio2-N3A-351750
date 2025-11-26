using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Repositorio;
using Estructura.InterfacesRepositorios;
using LogicaApp.DTO;
using LogicaApp.Mappers;
using LogicaApp.InterfacesCasosDeUso.Auditoria;

namespace LogicaApp.CasosDeUso.Auditoria
{
    public class AgregarAuditoriaCU : IAgregarAuditoria
    { 
        private IAuditoriaRepositorio repositorio;

        public AgregarAuditoriaCU(IAuditoriaRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void AgregarAuditoria(AuditoriaDTO nueva)
        {
            repositorio.Agregar(AuditoriaMapper.FromDTO(nueva));
        }
    }
}