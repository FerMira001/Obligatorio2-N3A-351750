using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estructura.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.EntityFramework
{
    public class ObligatorioContexto : DbContext
    {
        public DbSet<TipoGasto> TiposDeGasto { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<PagoUnico> PagosUnicos { get; set; }
        public DbSet<PagoRecurrente> PagosRecurrentes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }

        public ObligatorioContexto(DbContextOptions<ObligatorioContexto> options) : base(options)
        {
        }

    }
}
