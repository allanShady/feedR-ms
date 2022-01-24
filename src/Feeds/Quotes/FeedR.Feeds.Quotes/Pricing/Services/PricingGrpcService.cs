using System.Collections.Concurrent;
using FeedR.Feeds.Quotes.Pricing.Models;
using Grpc.Core;

namespace FeedR.Feeds.Quotes.Pricing.Services;

internal sealed class PricingGrpcService : PricingFeed.PricingFeedBase
{
    private readonly BlockingCollection<CurrencyPair> _currencyPairs = new();
    private readonly IPricingGenerator _pricingGenerator;

    public ILogger<PricingGrpcService> _logger { get; }
    public PricingGrpcService(IPricingGenerator pricingGenerator, ILogger<PricingGrpcService> logger)
    {
        _logger = logger;
        _pricingGenerator = pricingGenerator;
    }
    public override Task<GetSymbolsResponse> GetSymbols(GetSymbolsRequest request, ServerCallContext context)
    => Task.FromResult(new GetSymbolsResponse() { Symbols = { _pricingGenerator.GetSymbols() } });

    public override async Task SubscribePricing(PricingRequest request, IServerStreamWriter<PricingResponse> responseStream, ServerCallContext context)
    {
        _logger.LogInformation("started client streaming ...");

        _pricingGenerator.PricingUpdated += OnPricingUpdated;

        while (!context.CancellationToken.IsCancellationRequested)
        {
            if (!_currencyPairs.TryTake(out var currencyPair)) continue;
            if (!string.IsNullOrWhiteSpace(request.Symbol) && request.Symbol != currencyPair.Symbol)
                continue;

            await responseStream.WriteAsync(new PricingResponse
            {
                Symbol = currencyPair.Symbol,
                Value = (int)(100 * currencyPair.value),
                Timestamp = currencyPair.Timestamp
            });
        }

        _pricingGenerator.PricingUpdated -= OnPricingUpdated;
        _logger.LogInformation("stopped client streaming ...");

        void OnPricingUpdated(object? sender, CurrencyPair currencyPair) => _currencyPairs.TryAdd(currencyPair);
    }
}