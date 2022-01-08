namespace FeedR.Feeds.Quotes.Pricing.Services;

internal class PricingBackgroundService : IHostedService
{
    private readonly IPricingGenerator _pricingGenerator;

    public PricingBackgroundService(IPricingGenerator pricingGenerator) 
        => _pricingGenerator = pricingGenerator;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _pricingGenerator.startAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _pricingGenerator.stopAsync();
    }
}