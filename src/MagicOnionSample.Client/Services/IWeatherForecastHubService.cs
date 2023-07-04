using MagicOnionSample.Shared;

namespace MagicOnionSample.Client.Services;

public interface IWeatherForecastHubService
{
    ValueTask GetAsync(
        Action onStartCallback,
        Action<WeatherForecast> onGetCallback,
        Action onEndCallback);
}
