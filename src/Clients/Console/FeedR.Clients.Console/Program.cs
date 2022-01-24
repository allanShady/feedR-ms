using FeedR.Clients.Console;

using Grpc.Net.Client;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

using var channel = GrpcChannel.ForAddress("http://localhost:5041");
var client = new PricingFeed.PricingFeedClient(channel);

Console.WriteLine("Press any key to get symbols ...   ");
Console.ReadKey();

var symbolsResponse = await client.GetSymbolsAsync(new GetSymbolsRequest());
foreach (var symbol in symbolsResponse.Symbols)
    Console.WriteLine(symbol);

Console.WriteLine("provide a symbol (or leave empty): ");
var providedSymbol = Console.ReadLine()?.ToUpperInvariant();

if (!string.IsNullOrWhiteSpace(providedSymbol) && !symbolsResponse.Symbols.Contains(providedSymbol))
{
    Console.WriteLine($"Invalid provided symbol: . {providedSymbol}");
    return;
}

var pricingStream = client.SubscribePricing(new PricingRequest
{
    Symbol = providedSymbol
});

while (await pricingStream.ResponseStream.MoveNext(CancellationToken.None))
{
    var current = pricingStream.ResponseStream.Current;
    Console.WriteLine($"{DateTimeOffset.FromUnixTimeMilliseconds(current.Timestamp):T} -> {current.Symbol} = {current.Value / 100M}");
}