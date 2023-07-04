using Grpc.Core;
using MagicOnion.Client;
using MagicOnionSample.Shared;
using MagicOnionSample.Shared.Hubs;
using MagicOnionSample.Shared.Hubs.Receivers;

namespace MagicOnionSample.Client.Services.Implementations;

public sealed class WeatherForecastHubService : IWeatherForecastHubService, IWeatherForecastHubReceiver
{
    private readonly ChannelBase channelBase;

    private Action onStart;

    private Action<WeatherForecast> onGet;

    private Action onEnd;

    public WeatherForecastHubService(ChannelBase channelBase)
    {
        this.channelBase = channelBase;
        this.onStart = () => { };
        this.onGet = _ => { };
        this.onEnd = () => { };
    }

    public async ValueTask GetAsync(
        Action onStartCallback,
        Action<WeatherForecast> onGetCallback,
        Action onEndCallback)
    {
        this.onStart = onStartCallback;
        this.onGet = onGetCallback;
        this.onEnd = onEndCallback;
        var client = await StreamingHubClient
            .ConnectAsync<IWeatherForecastHub, IWeatherForecastHubReceiver>(this.channelBase, this);

        await client.StartAsync(Guid.NewGuid().ToString());
    }

    void IWeatherForecastHubReceiver.OnStart()
    {
        this.onStart();
    }

    void IWeatherForecastHubReceiver.OnGet(WeatherForecast weatherForecast)
    {
        this.onGet(weatherForecast);
    }

    void IWeatherForecastHubReceiver.OnEnd()
    {
        this.onEnd();
    }
}
