#!bin/bash     
     
dotnet run --project src/Aggregator/FeedR.Aggregator/FeedR.Aggregator.csproj 
dotnet run --project src/Aggregator/FeedR.Aggregator/FeedR.Aggregator.csproj --urls "http://*:5011"
dotnet run --project src/Aggregator/FeedR.Aggregator/FeedR.Aggregator.csproj --urls "http://*:5012"
      
dotnet run --project src/Notifier/FeedR.Notifier/FeedR.Notifier.csproj 

dotnet run --project src/Gateway/FeedR.Gateway/FeedR.Gateway.csproj 

dotnet run --project src/Feeds/News/FeedR.Feeds.News/FeedR.Feeds.News.csproj  
dotnet run --project src/Feeds/Weather/FeedR.Feeds.Weather/FeedR.Feeds.Weather.csproj  
dotnet run --project src/Feeds/Quotes/FeedR.Feeds.Quotes/FeedR.Feeds.Quotes.csproj 

#Create console sample client for GRPC endpoint
dotnet new console -n FeedR.Clients.Console -o src/Clients/Console/FeedR.Clients.Console 

##Console client add necessary reference
dotnet add package Google.Protobuf   
dotnet add package Grpc.Net.Client 
dotnet add package Grpc.Tools  

# add project to the solution
dotnet sln add src/Clients/Console/FeedR.Clients.Console/FeedR.Clients.Console.csproj   

# Install apache pulsar
dotnet add src/Shared/FeedR.Shared/FeedR.Shared.csproj package DotPulsar

# Start apache pulsar
docker run -it -p 6650:6650  -p 8080:8080 --mount source=pulsardata,target=/pulsar/data --mount source=pulsarconf,target=/pulsar/conf apachepulsar/pulsar:2.9.1 bin/pulsar standalone

## Add Xunit tests
dotnet new xunit --name FeedR.Feeds.News.Tests.EndToEnd 

# Add add newly created solution to the SLN
dotnet sln add src/Feeds/News/FeedR.Feeds.News.Tests.EndToEnd 