namespace FeedR.Shared.Streaming;

internal sealed class DefaultStreamPublisher : IStreamPublisher
{
    public Task PublihsAsync<T>(string topic, T data) where T : class => Task.CompletedTask;
}