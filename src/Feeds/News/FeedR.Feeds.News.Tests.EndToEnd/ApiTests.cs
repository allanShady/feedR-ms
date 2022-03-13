using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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
        var app = new NewTestApp();
        var client = app.CreateClient();
        var response = await client.GetAsync("/");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.ShouldBe("FeedR News feed");
    }
}