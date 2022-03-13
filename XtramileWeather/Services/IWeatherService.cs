using System;
using System.Threading.Tasks;
using XtramileWeather.Models;

namespace XtramileWeather.Services
{
    public interface IWeatherService
    {
        Task<CurrentWeatherModel> GetCurrentWeather(string cityName);
        Task<CurrentWeatherResponse> GetWeather(string cityName);
    }
}
