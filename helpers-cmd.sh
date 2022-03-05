#!bin/bash     
     
dotnet run -p src/Aggregator/FeedR.Aggregator/FeedR.Aggregator.csproj 
dotnet run -p src/Aggregator/FeedR.Aggregator/FeedR.Aggregator.csproj --urls "http://*:5011"
dotnet run -p src/Aggregator/FeedR.Aggregator/FeedR.Aggregator.csproj --urls "http://*:5012"
      
dotnet run -p src/Notifier/FeedR.Notifier/FeedR.Notifier.csproj 

dotnet run -p src/Gateway/FeedR.Gateway/FeedR.Gateway.csproj 

dotnet run -p src/Feeds/News/FeedR.Feeds.News/FeedR.Feeds.News.csproj  
dotnet run -p src/Feeds/Weather/FeedR.Feeds.Weather/FeedR.Feeds.Weather.csproj  
dotnet run -p src/Feeds/Quotes/FeedR.Feeds.Quotes/FeedR.Feeds.Quotes.csproj 

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