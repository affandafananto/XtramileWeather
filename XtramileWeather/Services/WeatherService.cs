using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using XtramileWeather.Models;

namespace XtramileWeather.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUtilityService _utilityService;

        public WeatherService(IHttpClientFactory httpClientFactory, IUtilityService utilityService)
        {
            _httpClientFactory = httpClientFactory;
            _utilityService = utilityService;
        }

        public async Task<CurrentWeatherModel> GetCurrentWeather(string cityName)
        {
            var response = await GetWeather(cityName);

            return new CurrentWeatherModel
            {
                Location = response.Name + ", " + response.Sys.Country,
                Coordinate = "lat: " + response.Coord.Lat + ",lon: " + response.Coord.Lon,
                Time = DateTimeOffset.FromUnixTimeSeconds(response.Dt).UtcDateTime.ToLocalTime(),
                Wind = "Speed: " + response.Wind.Speed + ", Deg: " + response.Wind.Deg,
                Visibility = response.Visibility,
                SkyConditions = response.Weather.FirstOrDefault() != null ?
                    response.Weather.First().Main + " - " + response.Weather.First().Description : string.Empty,
                TemperatureCelsius = _utilityService.ConvertFahrenheitToCelsius(response.Main.Temp).ToString(),
                TemperatureFahrenheit = response.Main.Temp.ToString(),
                DewPoint = string.Empty,
                Humidity = response.Main.Humidity.ToString(),
                Pressure = response.Main.Pressure.ToString()
            };
        }

        //API call
        public async Task<CurrentWeatherResponse> GetWeather(string cityName)
        {
            CurrentWeatherResponse response = null;

            try
            {
                var baseUrl = "https://api.openweathermap.org/data/2.5";
                var appId = "7a7951b4aa4301920ed5816c3dff454b";
                var url = baseUrl + "/weather?q=" + cityName + "&appid=" + appId + "&units=imperial";
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

                var httpClient = _httpClientFactory.CreateClient();
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<CurrentWeatherResponse>(contentStream);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}