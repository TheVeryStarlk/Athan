using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace Athan.UI;

internal sealed class NavigationService : INavigationService
{
    public Frame? Frame { get; set; }

    public void Navigate<T>() where T : INotifyPropertyChanged
    {
    }

    public void GoBack()
    {
        Frame?.GoBack();
    }
}

internal interface INavigationService
{
    public void Navigate<T>() where T : INotifyPropertyChanged;

    public void GoBack();
}