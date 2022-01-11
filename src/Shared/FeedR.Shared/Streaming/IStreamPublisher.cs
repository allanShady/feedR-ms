namespace FeedR.Shared.Streaming;

public interface IStreamPublisher
{
    Task PublihsAsync<T>(string topic, T data) where T : class;
}