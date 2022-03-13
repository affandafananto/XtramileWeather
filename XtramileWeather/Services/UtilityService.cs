namespace XtramileWeather.Services
{
    public class UtilityService : IUtilityService
    {
        public decimal ConvertFahrenheitToCelsius(decimal tempFahrenheit)
        {
            return (tempFahrenheit - 32) * 5 / 9;
        }
    }
}
