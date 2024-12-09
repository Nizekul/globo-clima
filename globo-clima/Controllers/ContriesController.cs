using globo_clima.Models;
using globo_clima.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace globo_clima.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CountriesController : Controller
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly CountryService _countryService;

        public CountriesController(ILogger<CountriesController> logger, CountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCountries()
        {
            try
            {
                var countriesData = await _countryService.GetAllCountriesAsync();
                return Ok(countriesData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados dos países");
                return StatusCode(500, "Erro interno ao buscar dados dos países.");
            }
        }

        [HttpGet("Page")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize]
        public async Task<IActionResult> GetAllCountriesByPage(List<CountryModel> allCountries, int currentPage, int itemsPerPage)
        {
            try
            {
                var countriesData = await _countryService.GetWeatherForCurrentPageAsync(allCountries, currentPage, itemsPerPage);
                return Ok(countriesData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados dos países");
                return StatusCode(500, "Erro interno ao buscar dados dos países.");
            }
        }
    }
}
