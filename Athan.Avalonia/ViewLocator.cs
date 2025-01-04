using System;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia;

internal sealed class ViewLocator(IServiceProvider services) : IDataTemplate
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
            return (Control) ActivatorUtilities.CreateInstance(services, type)!;
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