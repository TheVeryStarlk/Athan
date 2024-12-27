using System.IO;
using Athan.Desktop.Extensions;
using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Shell;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IWshRuntimeLibrary;
using Serilog;
using Wpf.Ui;
using Wpf.Ui.Controls;
using File = System.IO.File;
using MessageBox = Wpf.Ui.Controls.MessageBox;
using MessageBoxResult = Wpf.Ui.Controls.MessageBoxResult;
using NavigationService = Athan.Desktop.Features.Shell.NavigationService;

namespace Athan.Desktop.Features.Welcome;

public sealed partial class WelcomeViewModel(
    ILogger logger,
    LocationService locationService,
    SnackbarService snackbarService,
    SettingsService settingsService,
    NavigationService navigationService) : ObservableObject
{
    [RelayCommand]
    private async Task StartAsync()
    {
        var result = await locationService.GetLocationAsync();

        if (result.IsFailed)
        {
            snackbarService.Show(
                "Failed to retrieve location",
                result.Errors[0].Message,
                SymbolRegular.Warning20);

            return;
        }

        if (!settingsService.Exists("FirstTime"))
        {
            var messageBox = new MessageBox
            {
                Title = "Start Athan Automatically",
                Content = "Do you want Athan to start when your computer turns on?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };

            if (await messageBox.ShowDialogAsync() is MessageBoxResult.Primary)
            {
                AddToStartup();
            }

            settingsService.Set("FirstTime", false);
        }

        snackbarService.Show(
            "Location retrieved",
            $"You are in {result.Value}.",
            SymbolRegular.Location20);

        logger.Error("Saving and navigating to prayers...");

        settingsService.Set(nameof(Location), result.Value);
        navigationService.Navigate(Destination.Prayers);
    }

    private static void AddToStartup()
    {
        const string name = "Athan.lnk";

        File.Delete(name);

        var shell = new WshShell();

        var shortcut = shell.CreateShortcut(Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.Startup),
            name));

        var current = Environment.ProcessPath;

        shortcut.TargetPath = current;
        shortcut.WorkingDirectory = Path.GetDirectoryName(current);
        shortcut.Description = "Launches Athan.";

        shortcut.Save();
    }
}