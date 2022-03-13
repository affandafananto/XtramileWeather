using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XtramileWeather.Models
{
    public class WeatherViewModel
    {
        public IEnumerable<SelectListItem> Countries { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        public int CountryId { get; set; }

        public string CityId { get; set; }

    }
}
