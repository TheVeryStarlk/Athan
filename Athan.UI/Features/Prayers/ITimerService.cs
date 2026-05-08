using System;

namespace Athan.UI.Features.Prayers;

internal interface ITimerService
{
    void Start(TimeSpan interval, Action callback);

    void Stop();
}
