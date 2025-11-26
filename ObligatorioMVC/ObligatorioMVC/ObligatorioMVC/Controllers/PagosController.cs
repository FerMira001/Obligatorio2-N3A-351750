using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ObligatorioMVC.Models;

namespace ObligatorioMVC.Controllers
{
    public class PagosController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PagosController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // RF2 - Obtener mis pagos
        [HttpGet]
        public async Task<IActionResult> MisPagos()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            // Obtener usuario de la sesión
            var usuarioJson = HttpContext.Session.GetString("Usuario");
            var usuario = JsonSerializer.Deserialize<UsuarioDTO>(usuarioJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            try
            {
                var client = _httpClientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"Pagos/usuario/{usuario.Id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<MisPagosResponseDTO>(content, options);
                    return View(resultado);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    ViewBag.Error = "Error al obtener los pagos";
                    return View(new MisPagosResponseDTO());
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new MisPagosResponseDTO());
            }
        }

        // RF5 - Alta de pago único
        [HttpGet]
        public IActionResult CrearPagoUnico()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearPagoUnico(PagoUnicoDTO pago)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            try
            {
                var usuarioJson = HttpContext.Session.GetString("Usuario");
                var usuario = JsonSerializer.Deserialize<UsuarioDTO>(usuarioJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                pago.UsuarioId = usuario.Id;    

                var client = _httpClientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var json = JsonSerializer.Serialize(pago);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("Pagos/unico", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("MisPagos");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ViewBag.Error = $"Error al crear el pago: {errorContent}";
                    return View(pago);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pago);
            }
        }

        // RF5 - Alta de pago recurrente
        [HttpGet]
        public IActionResult CrearPagoRecurrente()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearPagoRecurrente(PagoRecurrenteDTO pago)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            try
            {
                var usuarioJson = HttpContext.Session.GetString("Usuario");
                var usuario = JsonSerializer.Deserialize<UsuarioDTO>(usuarioJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                pago.UsuarioId = usuario.Id;

                var client = _httpClientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var json = JsonSerializer.Serialize(pago);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("Pagos/recurrente", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("MisPagos");
                }
                else
                {
                    ViewBag.Error = "Error al crear el pago";
                    return View(pago);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pago);
            }
        }
    }
}