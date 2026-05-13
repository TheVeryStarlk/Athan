using System.Text.Json.Serialization;
using Siraj.Features.Locations;
using Siraj.Features.Prayers.Calculation;
using Siraj.Features.Settings;

namespace Siraj;

[JsonSourceGenerationOptions(NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(Location[]))]
[JsonSerializable(typeof(Theme))]
[JsonSerializable(typeof(CallReciter))]
[JsonSerializable(typeof(PrayerCalculation))]
[JsonSerializable(typeof(bool))]
internal sealed partial class SirajSerializerContext : JsonSerializerContext;