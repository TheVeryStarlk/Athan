using System.Text.Json;
using Athan.Avalonia;
using Athan.Avalonia.Models;
using Avalonia;

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