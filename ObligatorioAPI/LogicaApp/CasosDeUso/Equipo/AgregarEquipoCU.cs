using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.Equipo;
using Estructura.InterfacesRepositorios;
using LogicaApp.Mappers;

namespace LogicaApp.CasosDeUso.Equipo
{
    public class AgregarEquipoCU : IAgregarEquipo
    {
        private IEquipoRepositorio repositorio;

        public AgregarEquipoCU(IEquipoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void AgregarEquipo(EquipoDTO nuevo)
        {
            repositorio.Agregar(EquipoMapper.FromDTO(nuevo));
        }
    }
}