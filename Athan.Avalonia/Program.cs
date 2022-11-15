using Athan.Avalonia;
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

    await File
        .WriteAllTextAsync(Path.Join(directory, $"{Guid.NewGuid()}.txt"), exception.Message)
        .ConfigureAwait(false);

    Environment.Exit(-1);
}