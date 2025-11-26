using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ObligatorioMVC.Models;

namespace ObligatorioMVC.Controllers
{
    public class EquiposController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EquiposController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // RF4 - Equipos con pagos mayores a monto
        [HttpGet]
        public IActionResult PagosMayores()
        {
            var token = HttpContext.Session.GetString("Token");
            var rol = HttpContext.Session.GetInt32("Rol");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            // Solo gerentes
            if (rol != 1)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new List<EquipoDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> PagosMayores(decimal monto)
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

                var response = await client.GetAsync($"Equipos/pagos-mayores/{monto}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var equipos = JsonSerializer.Deserialize<List<EquipoDTO>>(content, options);

                    ViewBag.MontoBuscado = monto;
                    return View(equipos);
                }
                else
                {
                    ViewBag.Error = "Error al obtener los equipos";
                    return View(new List<EquipoDTO>());
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<EquipoDTO>());
            }
        }
    }
}