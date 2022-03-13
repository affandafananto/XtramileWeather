using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using XtramileWeather.Models;

namespace XtramileWeather.Services
{
    public interface IAreaService
    {
        IEnumerable<SelectListItem> GetCountries();

        IEnumerable<SelectListItem> GetCitiesByCountry(int countryId);

        List<AreaModel.City> GetCities();

        AreaModel.City GetCity(int id);
    }
}
