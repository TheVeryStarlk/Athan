using System;
using Microsoft.UI.Dispatching;

namespace Athan.UI.Features.Prayers;

internal sealed class DispatcherQueueTimerService : ITimerService
{
    private DispatcherQueueTimer? timer;
    private Action? callback;

    public void Start(TimeSpan interval, Action tick)
    {
        Stop();

        callback = tick;

        timer = DispatcherQueue.GetForCurrentThread().CreateTimer();
        timer.IsRepeating = false;
        timer.Interval = interval;
        timer.Tick += OnTick;
        timer.Start();
    }

    public void Stop()
    {
        if (timer is null)
        {
            return;
        }

        timer.Stop();
        timer.Tick -= OnTick;
        timer = null;
        callback = null;
    }

    private void OnTick(DispatcherQueueTimer sender, object args) => callback?.Invoke();
}
