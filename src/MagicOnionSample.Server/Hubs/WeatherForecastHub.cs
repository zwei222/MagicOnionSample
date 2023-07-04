using MagicOnion.Server.Hubs;
using MagicOnionSample.Shared;
using MagicOnionSample.Shared.Hubs;
using MagicOnionSample.Shared.Hubs.Receivers;

namespace MagicOnionSample.Server.Hubs;

public sealed class WeatherForecastHub : StreamingHubBase<IWeatherForecastHub, IWeatherForecastHubReceiver>, IWeatherForecastHub
{
    private readonly WeatherForecast[] weatherForecasts;

    private IGroup room;

    public WeatherForecastHub()
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
        this.room = default!;
    }

    public async ValueTask StartAsync(string roomId)
    {
        this.room = await this.Group.AddAsync(roomId);
        Broadcast(this.room).OnStart();

        foreach (var weatherForecast in this.weatherForecasts)
        {
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            Broadcast(this.room).OnGet(weatherForecast);
        }

        Broadcast(this.room).OnEnd();
    }
}
