using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ObligatorioMVC.Models;

namespace ObligatorioMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var json = JsonSerializer.Serialize(loginDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("Auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var loginResponse =
                        JsonSerializer.Deserialize<LoginResponseDTO>(responseContent, options);

                    if (loginResponse == null)
                    {
                        ViewBag.Error = "Respuesta inválida del servidor";
                        return View();
                    }

                    // Guardar token y usuario en sesión
                    HttpContext.Session.SetString("Token", loginResponse.Token);
                    HttpContext.Session.SetString("Usuario",
                        JsonSerializer.Serialize(loginResponse.Usuario));
                    HttpContext.Session.SetInt32("Rol", loginResponse.Usuario.Rol);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Credenciales inválidas";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
