using System;
using System.Globalization;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class DashboardViewModel : ObservableObject, INavigable
{
    public string Title => DateTime.Now.DayOfWeek.ToString();

    [ObservableProperty]
    private string? location;

    [ObservableProperty]
    private string? date;

    public void Navigated(Settings settings)
    {
        Location = settings.Location?.ToString();

        var calendar = new HijriCalendar();

        Date = DateTime.Parse($"{calendar.GetYear(DateTime.Now)}/" +
                              $"{calendar.GetMonth(DateTime.Now)}/" +
                              $"{calendar.GetDayOfMonth(DateTime.Now)}").ToShortDateString();
    }
}