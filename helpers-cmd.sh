#!bin/bash     
     
dotnet run -p src/Aggregator/FeedR.Aggregator/FeedR.Aggregator.csproj 
dotnet run -p src/Aggregator/FeedR.Aggregator/FeedR.Aggregator.csproj --urls "http://*:5011"
dotnet run -p src/Aggregator/FeedR.Aggregator/FeedR.Aggregator.csproj --urls "http://*:5012"
      
dotnet run -p src/Notifier/FeedR.Notifier/FeedR.Notifier.csproj 

dotnet run -p src/Gateway/FeedR.Gateway/FeedR.Gateway.csproj 

dotnet run -p src/Feeds/News/FeedR.Feeds.News/FeedR.Feeds.News.csproj  
dotnet run -p src/Feeds/Weather/FeedR.Feeds.Weather/FeedR.Feeds.Weather.csproj  
dotnet run -p src/Feeds/Quotes/FeedR.Feeds.Quotes/FeedR.Feeds.Quotes.csproj 