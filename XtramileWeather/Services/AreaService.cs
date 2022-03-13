using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using static XtramileWeather.Models.AreaModel;

namespace XtramileWeather.Services
{
    public class AreaService : IAreaService
    {
        public IEnumerable<SelectListItem> GetCountries()
        {
            return new List<SelectListItem> {
                    new SelectListItem { Text="United States", Value = "1" },
                    new SelectListItem { Text="United Kingdom", Value = "2" },
                };
        }

        public IEnumerable<SelectListItem> GetCitiesByCountry(int countryId)
        {
            return GetCities().Where(x => x.CountryId == countryId).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.CountryId.ToString()
            });
        }

        public List<City> GetCities()
        {
            return new List<City>() {
                new City{ Id = 1, CountryId = 1, Name = "New York"},
                new City{ Id = 2, CountryId = 2, Name = "London"},
            };
        }

        public City GetCity(int id)
        {
            return GetCities().SingleOrDefault(x => x.Id == id);
        }
    }
}