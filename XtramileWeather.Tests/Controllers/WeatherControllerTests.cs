using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XtramileWeather.Controllers;
using XtramileWeather.Models;
using XtramileWeather.Services;
using Xunit;
using static XtramileWeather.Models.AreaModel;

namespace XtramileWeather.Tests.Controllers
{
    public class WeatherControllerTests
    {
        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfCountries()
        {
            // Arrange
            var mockAreaService = new Mock<IAreaService>();
            mockAreaService.Setup(x => x.GetCountries()).Returns(GetCountries());
            var mockWeatherService = new Mock<IWeatherService>();

            var controller = new WeatherController(mockAreaService.Object, mockWeatherService.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<WeatherViewModel>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Countries.Count());
        }

        [Fact]
        public void GetCitiesByCountry_ReturnsAJson_WithAListCities()
        {
            var mockAreaService = new Mock<IAreaService>();
            mockAreaService.Setup(x => x.GetCitiesByCountry(1)).Returns(GetCitiesByCountry(1));
            var mockWeatherService = new Mock<IWeatherService>();

            var controller = new WeatherController(mockAreaService.Object, mockWeatherService.Object);

            var result = controller.GetCitiesByCountry(1);

            var jsonResult = Assert.IsType<JsonResult>(result);
            var cities = (IEnumerable<SelectListItem>)jsonResult.Value;

            Assert.Equal(2, cities.Count());
        }

        [Fact]
        public async Task GetCurrentWeather_ReturnsAJson_WithAWeatherObj()
        {
            var mockAreaService = new Mock<IAreaService>();
            mockAreaService.Setup(x => x.GetCity(1)).Returns(GetCity(1));
            var mockWeatherService = new Mock<IWeatherService>();
            mockWeatherService.Setup(x => x.GetCurrentWeather("New York")).Returns(GetCurrentWeather("New York"));
            var controller = new WeatherController(mockAreaService.Object, mockWeatherService.Object);

            var result = await controller.GetCurrentWeather(1);

            var jsonResult = Assert.IsType<JsonResult>(result);
            var cityCurrentWeather = (CurrentWeatherModel)jsonResult.Value;

            Assert.Equal("New York", cityCurrentWeather.Location);
        }

        private IEnumerable<SelectListItem> GetCountries()
        {
            return new List<SelectListItem> {
                    new SelectListItem { Text="United States", Value = "1" },
                    new SelectListItem { Text="United Kingdom", Value = "2" },
                };
        }

        private IEnumerable<SelectListItem> GetCitiesByCountry(int countryId)
        {
            return new List<SelectListItem> {
                    new SelectListItem { Text="New York", Value = "1" },
                    new SelectListItem { Text="Washington", Value = "2" }
                };
        }

        public City GetCity(int id)
        {
            return new City
            {
                Id = 1,
                CountryId = 1,
                Name = "New York"
            };
        }

        private async Task<CurrentWeatherModel> GetCurrentWeather(string cityName)
        {
            var task = Task.Run(() => new CurrentWeatherModel
            {
                Location = "New York"
            });

            return await task;
        }
    }
}