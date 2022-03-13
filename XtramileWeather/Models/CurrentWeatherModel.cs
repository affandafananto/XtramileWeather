using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XtramileWeather.Models
{
    public class CurrentWeatherModel
    {
        public string Location { get; set; }
        public string Coordinate { get; set; }
        public string Time { get; set; }
        public string Wind { get; set; }
        public string Visibility { get; set; }
        public string SkyConditions { get; set; }
        public string TemperatureCelsius { get; set; }
        public string TemperatureFahrenheit { get; set; }
        public string DewPoint { get; set; }
        public string Humidity { get; set; }
        public string Pressure { get; set; }
    }
}
