namespace FeedR.Shared.Messaging;

public class DefaultMessageSubscriber : IMessageSubscriber
{
    public Task SubscribeAsync<T>(string topic, Action<T> handler) where T : class, IMessage
        => Task.CompletedTask;
}