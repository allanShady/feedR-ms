using FeedR.Feeds.Quotes.Pricing.Models;

namespace FeedR.Feeds.Quotes.Pricing.Services;

internal sealed class PricingGenerator : IPricingGenerator
{
    private readonly Random _random = new();
    private readonly ILogger _logger;
    private bool _isRunning;
    public PricingGenerator(ILogger<PricingGenerator> logger) => _logger = logger;

    private readonly Dictionary<string, decimal> _currencyPairs = new()
    {
        ["EURMZN"] = 71.17M,
        ["EURZAR"] = 17.66M,
        ["EURUSD"] = 1.13M,
        ["EURGBP"] = 0.85M,
        ["EURCHF"] = 1.04M,
        ["EURPLN"] = 4.62M,
    };

    public event EventHandler<CurrencyPair>? PricingUpdated;

    public IEnumerable<string> GetSymbols() => _currencyPairs.Keys;

    public async IAsyncEnumerable<CurrencyPair> StartAsync()
    {
        _isRunning = true;
        while (_isRunning)
            foreach (var (symbol, pricing) in _currencyPairs)
            {
                if (!_isRunning) yield break;

                var tick = NextTick();
                var newPricing = pricing + tick;

                _currencyPairs[symbol] = newPricing;

                _logger.LogInformation($"Update pricing for: {symbol}, {pricing:F} -> {newPricing:F}");

                var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var currencyPair = new CurrencyPair(symbol, newPricing, timestamp);

                //fire pricing updated event
                PricingUpdated?.Invoke(this, currencyPair);

                yield return currencyPair;

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
    }

    public Task StopAsync()
    {
        _isRunning = false;
        return Task.CompletedTask;
    }

    private decimal NextTick()
    {
        var sign = _random.Next(0, 2) == 0 ? -1 : 1;
        var tick = _random.NextDouble() / 20;
        return (decimal)(sign * tick);
    }
}