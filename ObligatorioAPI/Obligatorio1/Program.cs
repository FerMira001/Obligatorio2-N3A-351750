using AccesoDatos.Repositorio;
using Estructura.InterfacesRepositorios;
using LogicaApp.CasosDeUso.PagoRecurrente;
using LogicaApp.CasosDeUso.PagoUnico;
using LogicaApp.CasosDeUso.Pago;
using LogicaApp.CasosDeUso.TipoGasto;
using LogicaApp.CasosDeUso.Usuario;
using LogicaApp.CasosDeUso.Equipo;
using LogicaApp.CasosDeUso.Auditoria;
using LogicaApp.InterfacesCasosDeUso.Equipo;
using LogicaApp.InterfacesCasosDeUso.TipoGasto;
using LogicaApp.InterfacesCasosDeUso.Usuario;
using LogicaApp.InterfacesCasosDeUso.Pago;
using LogicaApp.InterfacesCasosDeUso.PagoRecurrente;
using LogicaApp.InterfacesCasosDeUso.PagoUnico;
using LogicaApp.InterfacesCasosDeUso.Auditoria;
using AccesoDatos.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Obligatorio1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();

            builder.Services.AddDbContext<ObligatorioContexto>(options =>
                options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Obligatorio;Integrated Security=True;"));


            //Iniciando Repositorios
            builder.Services.AddScoped<ITipoGastoRepositorio, TiposDeGastoRepositorio>();
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IPagoRepositorio, PagoRepositorio>();
            builder.Services.AddScoped<IPagoUnicoRepositorio, PagoUnicoRepositorio>();
            builder.Services.AddScoped<IPagoRecurrenteRepositorio, PagoRecurrenteRepositorio>();
            builder.Services.AddScoped<IEquipoRepositorio, EquipoRepositorio>();
            builder.Services.AddScoped<IAuditoriaRepositorio, AuditoriaRepositorio>();

            //Iniciando Casos de Uso
            // Casos de uso TipoGasto
            builder.Services.AddScoped<IListarTiposDeGasto, ListarTiposGastoCU>();
            builder.Services.AddScoped<IAgregarTipoGasto, AgregarTipoGastoCU>();
            builder.Services.AddScoped<IActualizarTipoGasto, ActualizarTipoGastoCU>();
            builder.Services.AddScoped<IObtenerTipoGasto, ObtenerTipoGastoCU>();
            builder.Services.AddScoped<IQuitarTipoGasto, QuitarTipoGastoCU>();
            builder.Services.AddScoped<IObtenerTipoGastoPorNombre, ObtenerTipoGastoPorNombreCU>();

            builder.Services.AddScoped<IObtenerUsuarioPorMail, ObtenerUsuarioPorMailCU>();
            builder.Services.AddScoped<IAgregarUsuario, AgregarUsuarioCU>();
            builder.Services.AddScoped<IActualizarUsuario, ActualizarUsuarioCU>();
            builder.Services.AddScoped<IQuitarUsuario, QuitarUsuarioCU>();
            builder.Services.AddScoped<IListarUsuarios, ListarUsuariosCU>();
            builder.Services.AddScoped<IObtenerUsuario, ObtenerUsuarioCU>();
            builder.Services.AddScoped<IObtenerMontoDeUsuario, ObtenerMontoDeUsuarioCU>();

            builder.Services.AddScoped<IListarEquipos, ListarEquiposCU>();
            builder.Services.AddScoped<IObtenerEquipo, ObtenerEquipoCU>();

            builder.Services.AddScoped<ITipoGastoEnUso, TipoGastoEnUsoCU>();
            builder.Services.AddScoped<IAgregarPagoUnico, AgregarPagoUnicoCU>();
            builder.Services.AddScoped<IAgregarPagoRecurrente, AgregarPagoRecurrenteCU>();
            builder.Services.AddScoped<IListarPagosRecurrentes, ListarPagosRecurrentesCU>();
            builder.Services.AddScoped<IListarPagosUnicos, ListarPagosUnicosCU>();
            builder.Services.AddScoped<IListarPagosRecMes, ListarPagosRecMesCU>();
            builder.Services.AddScoped<IListarPagosUnicosMes, ListarPagosUnicosMesCU>();


            builder.Services.AddScoped<IAgregarAuditoria, AgregarAuditoriaCU>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Usuario}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
