using FluentAssertions;
using globo_clima.Controllers;
using globo_clima.Models;
using globo_clima.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace globo_clima.globo_clima.Tests.Controllers
{
    public class FavoritesControllerTest
    {
        private readonly Mock<IFavoritesService> _mockService;
        private readonly FavoritesController _controller;

        public FavoritesControllerTest()
        {
            _mockService = new Mock<IFavoritesService>();
            _controller = new FavoritesController(_mockService.Object);
        }

        [Fact]
        public async Task GetFavoritesByUserIdAsync_ReturnsOk_WhenFavoritesExist()
        {

            var userId = Guid.Parse("9ea39a47-34ef-4dfd-84c7-572badf89b8d");
            var mockFavorites = new List<FavoriteModel>
            {
                new FavoriteModel
                {
                    UserId = Guid.Parse("9ea39a47-34ef-4dfd-84c7-572badf89b8d"),
                    City = "Budapest",
                    Lat = "47",
                    Lon = "20",
                },
                new FavoriteModel
                {
                    UserId = Guid.Parse("9ea39a47-34ef-4dfd-84c7-572badf89b8d"),
                    City = "Freetown",
                    Lat = "8.5",
                    Lon = "-11.5",
                }
            };

            _mockService.Setup(s => s.GetFavoritesByUserIdAsync(userId))
                .ReturnsAsync(mockFavorites);
            var result = await _controller.GetFavoritesByUserIdAsync(userId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var favorites = Assert.IsType<List<FavoriteModel>>(okResult.Value);
            Assert.NotEmpty(favorites);
        }

        [Fact]
        public async Task GetFavoritesByUserIdAsync_ReturnsNotFound_WhenNoFavoritesExist()
        {
            var userId = new Guid();

            _mockService.Setup(s => s.GetFavoritesByUserIdAsync(userId))
                .ReturnsAsync(new List<FavoriteModel>());

            var result = await _controller.GetFavoritesByUserIdAsync(userId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Nenhum favorito encontrado.", notFoundResult.Value);
        }

    }
}
