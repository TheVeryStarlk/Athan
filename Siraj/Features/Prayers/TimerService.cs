using System;
using Microsoft.UI.Dispatching;

namespace Siraj.Features.Prayers;

internal sealed class TimerService(TimeProvider timeProvider)
{
    public event Action? Tick;

    private DispatcherQueueTimer? timer;

    public void Start()
    {
        if (timer is not null)
        {
            return;
        }

        timer = DispatcherQueue.GetForCurrentThread().CreateTimer();

        timer.Tick += OnTick;
        timer.Start();

        var elapsed = TimeSpan.FromTicks(timeProvider.GetLocalNow().TimeOfDay.Ticks % TimeSpan.FromMinutes(1).Ticks);
        var interval = elapsed == TimeSpan.Zero ? TimeSpan.FromMinutes(1) : TimeSpan.FromMinutes(1) - elapsed;
        
        timer.Interval = interval;
    }

    private void OnTick(DispatcherQueueTimer sender, object eventArgs)
    {
        Tick?.Invoke();
        timer?.Interval = TimeSpan.FromMinutes(1);
    }
}
