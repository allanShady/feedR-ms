using FeedR.Shared.Messaging;

namespace FeedR.Shared.Pulsar;

internal sealed class PulsarMessageSubscriber : IMessageSubscriber
{
    public Task SubscribeAsync<T>(string topic, Action<T> handler) where T : IMessage
    {
        throw new NotImplementedException();
    }
}
