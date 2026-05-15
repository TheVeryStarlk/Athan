using System;
using Microsoft.UI.Dispatching;

namespace Siraj.Features.Prayers;

internal sealed class TimerService
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
        timer.Interval = TimeSpan.FromSeconds(1);

        timer.Start();
    }

    private void OnTick(DispatcherQueueTimer sender, object eventArgs)
    {
        Tick?.Invoke();
    }
}
