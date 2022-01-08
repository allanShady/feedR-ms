namespace FeedR.Feeds.Quotes.Pricing.Services;

internal interface IPricingGenerator {
    Task startAsync();
    Task stopAsync();
}