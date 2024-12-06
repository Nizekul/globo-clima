using globo_clima.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace globo_clima.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CountriesController : Controller
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly HttpClient _httpClient;

        public CountriesController(ILogger<CountriesController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize]
        public async Task<IActionResult> GetAllContries()
        {
            try
            {
                string url = $"https://restcountries.com/v3.1/all?fields=name,capital,latlng,cca2";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Lê e deserializa o JSON retornado
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var contriesData = JsonSerializer.Deserialize<object>(jsonResponse); // Substitua 'object' pelo seu modelo            }

                return Ok(contriesData); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados Paises");
                return StatusCode(500, "Erro interno ao buscar Paises.");
            }
        }
    }
}
