using System.Linq;
using XtramileWeather.Services;
using Xunit;

namespace XtramileWeather.Tests.Services
{
    public class AreaServiceTests
    {
        [Fact]
        public void GetCountries_ReturnsAListOfCountries()
        {
            var service = new AreaService();
            var result = service.GetCountries();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count()); 
        }

        [Fact]
        public void GetCitiesByCountry_ReturnsASelectListOfCities()
        {
            var service = new AreaService();
            var result = service.GetCitiesByCountry(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetCities_ReturnsAListOfCity()
        {
            var service = new AreaService();
            var result = service.GetCities();

            Assert.NotNull(result);
        }

        [Fact]
        public void GetCity_ReturnsACityObj()
        {
            var service = new AreaService();
            var result = service.GetCity(1);

            Assert.NotNull(result);
            Assert.Equal("New York", result.Name);
        }
    }
}