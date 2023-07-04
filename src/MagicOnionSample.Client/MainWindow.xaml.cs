using System.Collections.ObjectModel;
using System.Windows;
using Grpc.Core;
using MagicOnion.Client;
using MagicOnionSample.Client.Services;
using MagicOnionSample.Shared;
using MagicOnionSample.Shared.Services;

namespace MagicOnionSample.Client;

public partial class MainWindow
{
    private readonly IWeatherForecastHubService weatherForecastHubService;

    private readonly IWeatherForecastService weatherForecastService;

    private readonly ObservableCollection<WeatherForecast> weatherForecastList;

    public MainWindow(ChannelBase channelBase, IWeatherForecastHubService weatherForecastHubService)
    {
        this.weatherForecastHubService = weatherForecastHubService;
        this.weatherForecastService = MagicOnionClient.Create<IWeatherForecastService>(channelBase);
        this.weatherForecastList = new ObservableCollection<WeatherForecast>();
        this.InitializeComponent();
        this.WeatherForecastListView.ItemsSource = this.weatherForecastList;
    }

    private async void OnClickGetButton(object sender, RoutedEventArgs e)
    {
        if (this.GetButton.IsEnabled is false)
        {
            return;
        }

        this.StartProcess();

        try
        {
            this.weatherForecastList.Clear();

            var weatherForecast = await this.GetAsync().ConfigureAwait(false);

            await this.Dispatcher.InvokeAsync(() =>
            {
                this.weatherForecastList.Add(weatherForecast);
            });
        }
        finally
        {
            await this.Dispatcher.InvokeAsync(this.EndProcess);
        }
    }

    private async void OnClickGetAllButton(object sender, RoutedEventArgs e)
    {
        if (this.GetAllButton.IsEnabled is false)
        {
            return;
        }

        this.StartProcess();

        try
        {
            this.weatherForecastList.Clear();
            var weatherForecasts = await this.GetAllAsync().ConfigureAwait(false);

            await this.Dispatcher.InvokeAsync(() =>
            {
                foreach (var weatherForecast in weatherForecasts)
                {
                    this.weatherForecastList.Add(weatherForecast);
                }
            });
        }
        finally
        {
            await this.Dispatcher.InvokeAsync(this.EndProcess);
        }
    }

    private async void OnClickGetAllHeavyButton(object sender, RoutedEventArgs e)
    {
        if (this.GetAllHeavyButton.IsEnabled is false)
        {
            return;
        }

        await this.weatherForecastHubService.GetAsync(
            async () =>
            {
                await this.Dispatcher.InvokeAsync(() =>
                {
                    this.StartProcess();
                    this.weatherForecastList.Clear();
                });
            },
            async weatherForecast =>
            {
                await this.Dispatcher.InvokeAsync(() =>
                {
                    this.weatherForecastList.Add(weatherForecast);
                });
            },
            async () =>
            {
                await this.Dispatcher.InvokeAsync(this.EndProcess);
            }).ConfigureAwait(false);
    }

    private async ValueTask<WeatherForecast> GetAsync()
    {
        return await this.weatherForecastService.GetAsync();
    }

    private async ValueTask<WeatherForecast[]> GetAllAsync()
    {
        return await this.weatherForecastService.GetAllAsync();
    }

    private void StartProcess()
    {
        this.GetButton.IsEnabled = false;
        this.GetAllButton.IsEnabled = false;
        this.GetAllHeavyButton.IsEnabled = false;
    }

    private void EndProcess()
    {
        this.GetButton.IsEnabled = true;
        this.GetAllButton.IsEnabled = true;
        this.GetAllHeavyButton.IsEnabled = true;
    }
}
