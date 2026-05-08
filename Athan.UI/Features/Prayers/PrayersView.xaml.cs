using System;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Athan.UI.Features.Prayers;

internal sealed partial class PrayersView : Page
{
    private DispatcherQueueTimer? refreshTimer;
    private bool waitingForFirstRefresh;

    public PrayersViewModel ViewModel
    {
        get
        {
            ArgumentNullException.ThrowIfNull(field);
            return field;
        }
        set;
    }

    public PrayersView()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs eventArgs)
    {
        ViewModel = (PrayersViewModel) eventArgs.Parameter;
        RefreshPrayers();
        StartRefreshTimer();

        base.OnNavigatedTo(eventArgs);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs eventArgs)
    {
        StopRefreshTimer();
        base.OnNavigatedFrom(eventArgs);
    }

    private void RefreshPrayers()
    {
        ViewModel.InitializeCommand.Execute(null);
    }

    private void StartRefreshTimer()
    {
        StopRefreshTimer();

        refreshTimer = DispatcherQueue.CreateTimer();
        refreshTimer.IsRepeating = true;
        refreshTimer.Interval = GetIntervalUntilNextMinute();
        refreshTimer.Tick += OnRefreshTimerTick;

        waitingForFirstRefresh = true;
        refreshTimer.Start();
    }

    private void StopRefreshTimer()
    {
        if (refreshTimer is null)
        {
            return;
        }

        refreshTimer.Stop();
        refreshTimer.Tick -= OnRefreshTimerTick;
        refreshTimer = null;
        waitingForFirstRefresh = false;
    }

    private void OnRefreshTimerTick(DispatcherQueueTimer sender, object args)
    {
        RefreshPrayers();

        if (!waitingForFirstRefresh)
        {
            return;
        }

        waitingForFirstRefresh = false;
        sender.Interval = TimeSpan.FromMinutes(1);
    }

    private static TimeSpan GetIntervalUntilNextMinute()
    {
        var now = DateTimeOffset.Now;
        var elapsed = TimeSpan.FromTicks(now.TimeOfDay.Ticks % TimeSpan.FromMinutes(1).Ticks);

        return elapsed == TimeSpan.Zero
            ? TimeSpan.FromMinutes(1)
            : TimeSpan.FromMinutes(1) - elapsed;
    }
}
