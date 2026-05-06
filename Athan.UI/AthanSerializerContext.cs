using System.Text.Json.Serialization;
using Athan.UI.Features.Locations;
using Athan.UI.Features.Settings;

namespace Athan.UI;

[JsonSourceGenerationOptions(NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(Location[]))]
[JsonSerializable(typeof(Theme))]
[JsonSerializable(typeof(Reciter))]
[JsonSerializable(typeof(bool))]
internal sealed partial class AthanSerializerContext : JsonSerializerContext;