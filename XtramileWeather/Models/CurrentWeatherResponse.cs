using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XtramileWeather.Models
{
    public class CurrentWeatherResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Coordinate Coord { get; set; }
        public IEnumerable<WeatherObj> Weather { get; set; }
        public MainObj Main { get; set; }
        public long Visibility { get; set; }
        public WindObj Wind { get; set; }
        public long Dt { get; set; }
        public SysObj Sys { get; set; }
    }

    public class Coordinate
    {
        public string Lat { get; set; }
        public string Lon { get; set; }
    }

    public class WeatherObj
    {
        public string Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class MainObj
    {
        public decimal Temp { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
    }

    public class WindObj
    {
        public string Speed { get; set; }
        public string Deg { get; set; }
    }

    public class SysObj
    {
        public string Country { get; set; }
    }
}
