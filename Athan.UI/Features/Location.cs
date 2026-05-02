using System.Text.Json.Serialization;

namespace Athan.UI.Features;

internal sealed class Location
{
    [JsonPropertyName("display_name")]
    public required string Name { get; init; }

    [JsonPropertyName("lat")]
    public required double Latitude { get; init; }

    [JsonPropertyName("lon")]
    public required double Longitude { get; init; }
}