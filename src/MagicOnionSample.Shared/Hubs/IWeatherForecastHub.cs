using MagicOnion;
using MagicOnionSample.Shared.Hubs.Receivers;

namespace MagicOnionSample.Shared.Hubs;

public interface IWeatherForecastHub : IStreamingHub<IWeatherForecastHub, IWeatherForecastHubReceiver>
{
    ValueTask StartAsync(string roomId);
}
