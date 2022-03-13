using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FeedR.Feeds.News.Tests.EndToEnd;

[ExcludeFromCodeCoverage]
internal sealed class NewTestApp : WebApplicationFactory<Program>
{
}