using System.Globalization;
using System.Text.Json;
using Athan.Avalonia;
using Athan.Avalonia.Models;
using Avalonia;

var setting = await File.ReadAllTextAsync(Path.Join(App.Directory, "Settings"));
var json = JsonSerializer.Deserialize<Setting>(setting);

// Force the language update before the application starts
Thread.CurrentThread.CurrentUICulture = new CultureInfo(json?.Language switch
{
    ApplicationLanguage.English or null => "en",
    ApplicationLanguage.Arabic => "ar",
    ApplicationLanguage.German => "de",
    _ => throw new ArgumentOutOfRangeException(nameof(json.Language))
});

try
{
    AppBuilder.Configure<App>()
        .UsePlatformDetect()
        .LogToTrace()
        .StartWithClassicDesktopLifetime(args);
}
catch (Exception exception)
{
    var directory = Path.Join(App.Directory, "Crashes");
    Directory.CreateDirectory(directory);

    var crash = JsonSerializer.Serialize(new Crash(DateTime.UtcNow, exception.Message));

    await File
        .WriteAllTextAsync(Path.Join(directory, $"{Guid.NewGuid()}.txt"), crash)
        .ConfigureAwait(false);

    Environment.Exit(-1);
}