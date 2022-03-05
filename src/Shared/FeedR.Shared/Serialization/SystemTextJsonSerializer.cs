using System.Text.Json;
using System.Text.Json.Serialization;

namespace FeedR.Shared.Serialization;

internal sealed class SystemTextJsonSerializer : ISerializer
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public T? Deserialize<T>(string value) where T : class => JsonSerializer.Deserialize<T>(value, options);

    public T? DeserializeBytes<T>(byte[] value) where T : class => JsonSerializer.Deserialize<T>(value, options);

    public string Serialize<T>(T value) where T : class => JsonSerializer.Serialize(value, options);

    public byte[] SerializeBytes<T>(T value) where T : class => JsonSerializer.SerializeToUtf8Bytes(value, options);
}