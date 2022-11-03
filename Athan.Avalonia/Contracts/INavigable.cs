using System.Threading.Tasks;
using Athan.Avalonia.Models;

namespace Athan.Avalonia.Contracts;

internal interface INavigable
{
    public string Title { get; }

    public Task Navigated(Setting setting)
    {
        return Task.CompletedTask;
    }
}