using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Athan.UI.Features.Settings;

internal sealed class StartupService
{
    public async Task<bool> TryToggle(bool value)
    {
        var task = await StartupTask.GetAsync("AthanStartupTask");
     
        if (value)
        {
            var result = await task.RequestEnableAsync();
            return result is StartupTaskState.Enabled;
        }
        else
        {
            task.Disable();
            return true;
        }
    }
}