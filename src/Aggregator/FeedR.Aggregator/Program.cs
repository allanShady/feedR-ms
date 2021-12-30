var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async ctx => 
{
    await ctx.Response.WriteAsync($"FeedR Aggregator, request ID:, {ctx.Request.Headers["x-request-id"]}");
}
);

app.Run(); 