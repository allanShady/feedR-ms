namespace FeedR.Shared.Messaging;

public class DefaultMessageSubscriber : IMessageSubscriber
{
    public Task SubscribeAsync<T>(string topic, Action<MessageEnvelope<T>> handler) where T : class, IMessage
        => Task.CompletedTask;
}