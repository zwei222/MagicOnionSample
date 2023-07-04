using MessagePack;

namespace MagicOnionSample.Shared;

[MessagePackObject]
public sealed class WeatherForecast
{
    [Key(0)]
    public DateTimeOffset Date { get; set; }

    [Key(1)]
    public int TemperatureC { get; set; }

    [Key(2)]
    public int TemperatureF => 32 + (int)(this.TemperatureC / 0.5556);

    [Key(3)]
    public string Summary { get; set; } = string.Empty;
}
