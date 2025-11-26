using LogicaApp.DTO;
using LogicaApp.InterfacesCasosDeUso.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ObligatorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IObtenerUsuarioPorMail _obtenerUsuarioPorMail;
        private readonly IConfiguration _configuration;
        private readonly ILoginUsuario _loginUsuario;


        public AuthController(
            IObtenerUsuarioPorMail obtenerUsuarioPorMail,
            IConfiguration configuration,
            ILoginUsuario loginUsuario)
        {
            _obtenerUsuarioPorMail = obtenerUsuarioPorMail;
            _configuration = configuration;
            _loginUsuario = loginUsuario;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                var usuario = _loginUsuario.Login(loginDto.Mail, loginDto.Password);

                if (usuario == null)
                {
                    return Unauthorized(new { mensaje = "Credenciales inválidas" });
                }

                var secretKey = _configuration["JwtSettings:SecretKey"];
                var token = TokenHandler.GenerarToken(usuario, secretKey);

                return Ok(new { token, usuario });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { mensaje = "Credenciales inválidas" });
            }
        }


        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return Ok(new { mensaje = "Sesión cerrada" });
        }
    }
}