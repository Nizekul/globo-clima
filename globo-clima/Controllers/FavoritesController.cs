using globo_clima.Models;
using globo_clima.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;


namespace globo_clima.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class FavoritesController : Controller
    {
        private readonly FavoritesService _favoritesService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<CountriesController> _logger;

        public FavoritesController(FavoritesService favoritesService, HttpClient httpClient, ILogger<CountriesController> logger)
        {
            _favoritesService = favoritesService;
            _httpClient = httpClient;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllFavoritesAsync()
        {
            var favorites = await _favoritesService.GetAllFavoritesAsync();

            if (favorites == null || favorites.Count == 0)
            {
                return NotFound("Nenhum favorito encontrado.");
            }

            return Ok(favorites);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFavoriteAsync([FromBody] List<FavoritesModel> favorites)
        {
            var favoritesResult = await _favoritesService.CreateFavorites(favorites);

            return Ok(favoritesResult);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteFavoriteAsync([FromBody] List<Guid> favorites)
        {
            var favoritesResult = await _favoritesService.DeleteFavorites(favorites);

            return Ok(favoritesResult);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateFavoriteAsync([FromBody] List<Guid> favorites)
        {
            var favoritesResult = await _favoritesService.DeleteFavorites(favorites);

            return Ok(favoritesResult);
        }
    }
}
