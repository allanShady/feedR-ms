using Microsoft.AspNetCore.Builder;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("yarp"))
    .AddTransforms(transforms => 
    {
        transforms.AddRequestTransform(transform => 
        {
            var requestId = Guid.NewGuid().ToString();
            transform.ProxyRequest.Headers.Add("x-request-id", requestId);
            return ValueTask.CompletedTask;
        });
        
    });

var app = builder.Build();

app.MapGet("/", () => "FeedR Gateway");
app.MapReverseProxy();

app.Run();