using System.Text.Json.Serialization;
using FeedR.Feeds.Weather.Models;

namespace FeedR.Feeds.Weather.Services;
internal sealed class WeatherFeed : IWeatherFeed
{
    //TODO: refactor - load this data from configuration or from env var
    private const string ApiKey = "secret";
    private const string ApiUrl = "secret";
    private readonly HttpClient _client;
    private readonly ILogger<WeatherFeed> _logger;

    public WeatherFeed(HttpClient client, ILogger<WeatherFeed> logger)
    {
        _client = client;
        _logger = logger;
    }
    public async IAsyncEnumerable<WeatherData> SubscribeAsync(string location, CancellationToken cancellationToken)
    {
        var url = $"{ApiUrl}?key={ApiKey}&q={location}&aqi=no";

        while (!cancellationToken.IsCancellationRequested)
        {
            var response = await _client.GetFromJsonAsync<WeatherResponse>(url, cancellationToken);

            if (response is null) continue;

            yield return new WeatherData($"{response.Location.Name}, {response.Location.Country}",
                response.Current.TempC, response.Current.Humidity, response.Current.WindKph,
                response.Current.Condition.Text);

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        }
    }

    private record WeatherResponse(Location Location, Weather Current);

    private record Location(string Name, string Country);
    private record Condition(string Text);
    private record Weather([property: JsonPropertyName("temp_c")] double TempC,
        [property: JsonPropertyName("wind_kph")] double WindKph, double Humidity, Condition Condition);
}
