using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XtramileWeather.Models;
using XtramileWeather.Services;

namespace XtramileWeather.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IAreaService _areaService;
        private readonly IWeatherService _weatherService;

        public WeatherController(IAreaService areaService, IWeatherService weatherService)
        {
            _areaService = areaService;
            _weatherService = weatherService;
        }

        // GET: WeatherController
        public ActionResult Index()
        {
            var weatherViewModel = new WeatherViewModel();
            weatherViewModel.Countries = _areaService.GetCountries();

            return View(weatherViewModel);
        }

        public ActionResult GetCitiesByCountry(int countryId)
        {
            var cities = _areaService.GetCitiesByCountry(countryId);

            return Json(cities);
        }

        public async Task<ActionResult> GetCurrentWeather(int cityId)
        {
            var city = _areaService.GetCity(cityId);
            var result = await _weatherService.GetCurrentWeather(city.Name);

            return Json(result);
        }
    }
}