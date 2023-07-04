namespace MagicOnionSample.Shared.Hubs.Receivers;

public interface IWeatherForecastHubReceiver
{
    void OnStart();

    void OnGet(WeatherForecast weatherForecast);

    void OnEnd();
}
