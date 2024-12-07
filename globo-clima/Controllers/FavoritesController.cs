using globo_clima.Models;
using globo_clima.Services;
using Microsoft.AspNetCore.Mvc;

namespace globo_clima.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class FavoritesController : Controller
    {
        private readonly FavoritesService _favoritesService;

        public FavoritesController(FavoritesService favoritesService, ILogger<CountriesController> logger)
        {
            _favoritesService = favoritesService;
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
        public async Task<IActionResult> CreateFavoriteAsync([FromBody] FavoritesModel favorites)
        {
            var favoritesResult = await _favoritesService.CreateFavorites(favorites);

            return Ok(favoritesResult);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteFavoriteAsync([FromQuery] Guid favoriteID, [FromQuery] Guid userID)
        {
            var favoritesResult = await _favoritesService.DeleteFavorites(favoriteID, userID);

            return Ok(favoritesResult);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateFavoriteAsync([FromQuery] List<FavoritesModel> favorites, [FromQuery] Guid userID)
        {
            var favoritesResult = await _favoritesService.UpdateFavoriteAsync(favorites, userID);

            return Ok(favoritesResult);
        }
    }
}
