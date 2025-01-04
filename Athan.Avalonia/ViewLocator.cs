using System;
using System.ComponentModel;
using Athan.Avalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Athan.Avalonia;

internal sealed class ViewLocator : IDataTemplate
{
    public Control? Build(object? paramater)
    {
        if (paramater is null)
        {
            return null;
        }

        var name = paramater.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type is not null)
        {
            return (Control) Activator.CreateInstance(type)!;
        }

        return new TextBlock
        {
            Text = name
        };
    }

    public bool Match(object? data)
    {
        return data is INotifyPropertyChanged;
    }
}