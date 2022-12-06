using System.Collections.Generic;

namespace Like.LikeYou.Controllers
{
    public interface IWeatherForecastController
    {
        IEnumerable<WeatherForecast> Get();
    }
}