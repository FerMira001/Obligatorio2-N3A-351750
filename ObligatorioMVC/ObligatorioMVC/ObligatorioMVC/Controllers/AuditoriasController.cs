using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ObligatorioMVC.Models;

namespace ObligatorioMVC.Controllers
{
    public class AuditoriasController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuditoriasController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // RF6 - Auditorías por tipo de gasto
        [HttpGet]
        public IActionResult PorTipoGasto()
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

            return View(new List<AuditoriaDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> PorTipoGasto(int tipoGastoId)
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

                var response = await client.GetAsync($"Auditorias/tipogasto/{tipoGastoId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var auditorias = JsonSerializer.Deserialize<List<AuditoriaDTO>>(content, options);

                    ViewBag.TipoGastoId = tipoGastoId;
                    return View(auditorias);
                }
                else
                {
                    ViewBag.Error = "Error al obtener las auditorías";
                    return View(new List<AuditoriaDTO>());
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<AuditoriaDTO>());
            }
        }
    }
}