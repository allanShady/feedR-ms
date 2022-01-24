using FeedR.Feeds.Quotes.Pricing;

using Grpc.Net.Client;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

using var channel = GrpcChannel.ForAddress("http://localhost:5041");
var client = new PricingFeed.PricingFeedClient(channel);

Console.WriteLine("Press any key to get symbols ...");
Console.ReadKey();

var symbolsResponse = await client.GetSymbolsAsync(new GetSymbolsRequest());
foreach (var symbol in symbolsResponse.Symbols)
    Console.WriteLine(symbol);