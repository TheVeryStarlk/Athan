using System.Text.Json.Serialization;
using Athan.UI.Features.Locations;

namespace Athan.UI;

[JsonSourceGenerationOptions(NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(Location[]))]
internal sealed partial class AthanSerializerContext : JsonSerializerContext;