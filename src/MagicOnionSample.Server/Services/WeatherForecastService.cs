using MagicOnion;
using MagicOnion.Server;
using MagicOnionSample.Shared;
using MagicOnionSample.Shared.Services;

namespace MagicOnionSample.Server.Services;

public sealed class WeatherForecastService : ServiceBase<IWeatherForecastService>, IWeatherForecastService
{
    private readonly WeatherForecast[] weatherForecasts;

    public WeatherForecastService()
    {
        this.weatherForecasts = new[]
        {
            new WeatherForecast
            {
                Date = DateTimeOffset.Parse("2019-07-16T19:04:05.7257911-06:00"),
                TemperatureC = 52,
                Summary = "Mild",
            },
            new WeatherForecast
            {
                Date = DateTimeOffset.Parse("2019-07-17T19:04:05.7258461-06:00"),
                TemperatureC = 36,
                Summary = "Warm",
            },
            new WeatherForecast
            {
                Date = DateTimeOffset.Parse("2019-07-18T19:04:05.7258467-06:00"),
                TemperatureC = 39,
                Summary = "Cool",
            },
            new WeatherForecast
            {
                Date = DateTimeOffset.Parse("2019-07-19T19:04:05.7258471-06:00"),
                TemperatureC = 10,
                Summary = "Bracing",
            },
            new WeatherForecast
            {
                Date = DateTimeOffset.Parse("2019-07-20T19:04:05.7258474-06:00"),
                TemperatureC = -1,
                Summary = "Chilly",
            },
        };
    }

    public async UnaryResult<WeatherForecast> GetAsync()
    {
        await Task.CompletedTask.ConfigureAwait(false);
        return this.weatherForecasts.First();
    }

    public async UnaryResult<WeatherForecast[]> GetAllAsync()
    {
        await Task.CompletedTask.ConfigureAwait(false);
        return this.weatherForecasts;
    }
}
