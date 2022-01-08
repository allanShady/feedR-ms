using FeedR.Feeds.Quotes.Pricing.Requests;

namespace FeedR.Feeds.Quotes.Pricing.Services;

internal class PricingBackgroundService : BackgroundService
{
    ILogger<PricingBackgroundService> _logger;
    private int _runningStatus; // 0 or 1
    private readonly IPricingGenerator _pricingGenerator;
    private readonly PricingRequestsChannel _requestsChannel;

    public PricingBackgroundService(IPricingGenerator pricingGenerator, PricingRequestsChannel requestsChannel,
        ILogger<PricingBackgroundService> logger
    ) {
        _logger = logger;
        _requestsChannel = requestsChannel;
        _pricingGenerator = pricingGenerator;
    }

    public Task StartAsync(CancellationToken cancellationToken) => _ = _pricingGenerator.StartAsync();

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _pricingGenerator.StopAsync();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Pricing background service has started.");
        await foreach(var request in _requestsChannel.Requests.Reader.ReadAllAsync(stoppingToken)) 
        {
            _logger.LogInformation($"Pricing background service has received the request. {request.GetType().Name}");

            var _ = request switch {
                StartPricing => StartGenatorAsync(),
                StopPricing => StopGenatorAsync(),
                _ => Task.CompletedTask
            };
        }
    }

    private async Task StartGenatorAsync() {
        if (Interlocked.Exchange(ref _runningStatus, 1) == 1) {
            _logger.LogInformation("Pricing generator is already running");
            return;
        }
 
        await _pricingGenerator.StartAsync();
    }
    private async Task StopGenatorAsync() {
        if (Interlocked.Exchange(ref _runningStatus, 0) == 0) { 
             _logger.LogInformation("Pricing generator is not running");
            return;
        }
        
        await _pricingGenerator.StopAsync();
    }
}