using AccesoDatos.Repositorio;
using AccesoDatos.EntityFramework;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ObligatorioAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers
            builder.Services.AddControllers();

            // Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "Obligatorio API", Version = "v1" });

                // Configuración para JWT en Swagger
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header usando el esquema Bearer. Ejemplo: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // DbContext
            builder.Services.AddDbContext<ObligatorioContexto>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ObligatorioConnection")));

            // JWT Authentication

            var secretKey = builder.Configuration["JwtSettings:SecretKey"];

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });

            builder.Services.AddAuthorization();

            // Repositorios
            builder.Services.AddScoped<ITipoGastoRepositorio, TiposDeGastoRepositorio>();
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IPagoRepositorio, PagoRepositorio>();
            builder.Services.AddScoped<IPagoUnicoRepositorio, PagoUnicoRepositorio>();
            builder.Services.AddScoped<IPagoRecurrenteRepositorio, PagoRecurrenteRepositorio>();
            builder.Services.AddScoped<IEquipoRepositorio, EquipoRepositorio>();
            builder.Services.AddScoped<IAuditoriaRepositorio, AuditoriaRepositorio>();

            // Casos de Uso - TipoGasto
            builder.Services.AddScoped<IListarTiposDeGasto, ListarTiposGastoCU>();
            builder.Services.AddScoped<IAgregarTipoGasto, AgregarTipoGastoCU>();
            builder.Services.AddScoped<IActualizarTipoGasto, ActualizarTipoGastoCU>();
            builder.Services.AddScoped<IObtenerTipoGasto, ObtenerTipoGastoCU>();
            builder.Services.AddScoped<IQuitarTipoGasto, QuitarTipoGastoCU>();
            builder.Services.AddScoped<IObtenerTipoGastoPorNombre, ObtenerTipoGastoPorNombreCU>();

            // Casos de Uso - Usuario
            builder.Services.AddScoped<IObtenerUsuarioPorMail, ObtenerUsuarioPorMailCU>();
            builder.Services.AddScoped<IAgregarUsuario, AgregarUsuarioCU>();
            builder.Services.AddScoped<IActualizarUsuario, ActualizarUsuarioCU>();
            builder.Services.AddScoped<IQuitarUsuario, QuitarUsuarioCU>();
            builder.Services.AddScoped<IListarUsuarios, ListarUsuariosCU>();
            builder.Services.AddScoped<IObtenerUsuario, ObtenerUsuarioCU>();
            builder.Services.AddScoped<IObtenerMontoDeUsuario, ObtenerMontoDeUsuarioCU>();

            // Casos de Uso - Equipo
            builder.Services.AddScoped<IListarEquipos, ListarEquiposCU>();
            builder.Services.AddScoped<IObtenerEquipo, ObtenerEquipoCU>();

            // Casos de Uso - Pagos
            builder.Services.AddScoped<ITipoGastoEnUso, TipoGastoEnUsoCU>();
            builder.Services.AddScoped<IAgregarPagoUnico, AgregarPagoUnicoCU>();
            builder.Services.AddScoped<IAgregarPagoRecurrente, AgregarPagoRecurrenteCU>();
            builder.Services.AddScoped<IListarPagosRecurrentes, ListarPagosRecurrentesCU>();
            builder.Services.AddScoped<IListarPagosUnicos, ListarPagosUnicosCU>();
            builder.Services.AddScoped<IListarPagosRecMes, ListarPagosRecMesCU>();
            builder.Services.AddScoped<IListarPagosUnicosMes, ListarPagosUnicosMesCU>();

            // Casos de Uso - Auditoria
            builder.Services.AddScoped<IAgregarAuditoria, AgregarAuditoriaCU>();



            // CASOS DE USO AGREGADOS PARA EL OBLIGATORIO 2
            builder.Services.AddScoped<IListarPagosUnicosPorUsuario, ListarPagosUnicosPorUsuarioCU>();
            builder.Services.AddScoped<IListarPagosRecurrentesPorUsuario, ListarPagosRecurrentesPorUsuarioCU>();

            builder.Services.AddScoped<IListarEquiposConPagosUnicosMayorA, ListarEquiposConPagosUnicosMayorACU>();

            builder.Services.AddScoped<IListarAuditoriasPorTipoGasto, ListarAuditoriasPorTipoGastoCU>();

            builder.Services.AddScoped<ILoginUsuario, LoginUsuarioCU>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}