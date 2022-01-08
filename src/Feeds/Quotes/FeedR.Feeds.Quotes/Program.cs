using FeedR.Feeds.Quotes.Pricing.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IPricingGenerator, PricingGenerator>()
    .AddHostedService<PricingBackgroundService>();

var app = builder.Build();

app.MapGet("/", () => "FeedR Quotes feed");

app.Run();