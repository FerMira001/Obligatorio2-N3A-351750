
using AccesoDatos.EntityFramework;
using AccesoDatos.Repositorio;
using Estructura.InterfacesRepositorios;
using LogicaApp.InterfacesCasosDeUso.Pago;
using LogicaApp.CasosDeUso.Pago;
using Microsoft.EntityFrameworkCore;

namespace WebApiPagos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //Configuracion de Base de datos
            builder.Services.AddDbContext<ObligatorioContexto>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Obligatorio")
                )
            );

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddScoped<IPagoRepositorio, PagoRepositorio>();

            builder.Services.AddScoped<IObtenerPago, ObtenerPagoCU>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
