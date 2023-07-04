using MagicOnion;

namespace MagicOnionSample.Shared.Services;

public interface IWeatherForecastService : IService<IWeatherForecastService>
{
    UnaryResult<WeatherForecast> GetAsync();

    UnaryResult<WeatherForecast[]> GetAllAsync();
}
