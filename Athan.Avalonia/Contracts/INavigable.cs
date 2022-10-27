using Athan.Avalonia.Models;

namespace Athan.Avalonia.Contracts;

internal interface INavigable
{
    public string Title { get; }

    public void Navigated(Settings settings)
    {
    }
}