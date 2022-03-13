using XtramileWeather.Services;
using Xunit;

namespace XtramileWeather.Tests.Services
{
    public class UtilityServiceTests
    {
        [Fact]
        public void ConvertFahrenheitToCelsius_ReturnsTempInCelcius()
        {
            var service = new UtilityService();
            var result = service.ConvertFahrenheitToCelsius(50);

            Assert.Equal(10, result);

        }
    }
}