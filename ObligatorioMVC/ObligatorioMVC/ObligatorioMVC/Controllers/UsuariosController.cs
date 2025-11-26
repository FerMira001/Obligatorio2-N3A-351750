using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ObligatorioMVC.Models;

namespace ObligatorioMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UsuariosController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // RF3 - Resetear contraseña
        [HttpGet]
        public IActionResult ResetearPassword()
        {
            var token = HttpContext.Session.GetString("Token");
            var rol = HttpContext.Session.GetInt32("Rol");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            // Solo administradores
            if (rol != 0)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetearPassword(int usuarioId)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            try
            {
                var client = _httpClientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.PostAsync($"Usuarios/resetpassword/{usuarioId}", null);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<ResetPasswordResponseDTO>(content, options);

                    ViewBag.NuevaPassword = resultado.NuevaPassword;
                    ViewBag.Mensaje = "Contraseña reseteada correctamente";
                    return View();
                }
                else
                {
                    ViewBag.Error = "Error al resetear la contraseña";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}   