using Athan.UI.Features.Search;
using System.Text.Json.Serialization;

namespace Athan.UI.Features;

[JsonSourceGenerationOptions(NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(Location[]))]
internal sealed partial class AthanSerializerContext : JsonSerializerContext;