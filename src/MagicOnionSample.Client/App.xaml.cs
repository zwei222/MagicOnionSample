using System.Windows;
using Grpc.Core;
using Grpc.Net.Client;
using MagicOnionSample.Client.Services;
using MagicOnionSample.Client.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MagicOnionSample.Client;

public sealed partial class App
{
    private readonly IHost host;

    public App()
    {
        this.host = Host
            .CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<ChannelBase>(GrpcChannel.ForAddress(@"https://localhost:5001"));
                services.AddSingleton<IWeatherForecastHubService, WeatherForecastHubService>();
                services.AddTransient<MainWindow>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await this.host.StartAsync().ConfigureAwait(false);
        this.MainWindow = this.host.Services.GetRequiredService<MainWindow>();
        this.MainWindow.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await this.host.StopAsync().ConfigureAwait(false);
        this.host.Dispose();
        base.OnExit(e);
    }
}
