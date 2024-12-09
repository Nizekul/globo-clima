using FluentAssertions;
using globo_clima.Controllers;
using globo_clima.Models;
using globo_clima.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace globo_clima.globo_clima.Tests
{
    public class WeatherControllerTest
    {
        private readonly Mock<IWeatherService> _mockService;
        private readonly WeatherController _controller;
        private readonly Mock<ILogger<WeatherController>> _mockLogger;

        public WeatherControllerTest()
        {
            _mockService = new Mock<IWeatherService>();
            _mockLogger = new Mock<ILogger<WeatherController>>();
            _controller = new WeatherController(_mockLogger.Object, _mockService.Object);
        }

        [Fact]
        public async Task GetWeatherByCity_ReturnsOk_WhenWeatherDataExists()
        {
            var weatherCity = "Lajedo";

            _mockService.Setup(s => s.GetWeatherByCityAsync(weatherCity));

            var result = await _controller.GetWeatherByCity(weatherCity);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
