using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FeedR.Feeds.News.Messages;
using FeedR.Shared.Streaming;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace FeedR.Feeds.News.Tests.EndToEnd;

[ExcludeFromCodeCoverage]
public sealed class ApiTests
{
    [Fact]
    public async Task get_base_endpoint_should_return_ok_status_code_and_service_name()
    {
        var response = await _client.GetAsync("/");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.ShouldBe("FeedR News feed");
    }

    [Fact]
    public async void post_news_should_return_accepted_status_code_and_publish_news_published_event()
    {
        // Given
        var tcs = new TaskCompletionSource<NewsPublished>();
        var subscriber = _app.Services.GetRequiredService<IStreamSubscriber>();

        await subscriber.SubscribeAsync<NewsPublished>("news", message =>
        {
            tcs.SetResult(message);
        });
        var request = new PublishNews("Kuambe Cyclone", "Cyclone");

        // When
        var response = await _client.PostAsJsonAsync("news", request);

        // Then
        response.StatusCode.ShouldBe(HttpStatusCode.Accepted);

        var @event = await tcs.Task;
        @event.Title.ShouldBe(request.Title);
        @event.Category.ShouldBe(request.Category);
    }

    private readonly NewTestApp _app;
    private readonly HttpClient _client;

    public ApiTests()
    {
        _app = new NewTestApp();
        _client = _app.CreateClient();
    }
}