using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Athan.Avalonia;

internal sealed class ViewLocator : IDataTemplate
{
    public IControl Build(object data)
    {
        var name = data.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);

        return type is not null
            ? (Control) Activator.CreateInstance(type)!
            : new TextBlock()
            {
                Text = $"Could not find the view: '{name}'."
            };
    }

    public bool Match(object data)
    {
        return data is INotifyPropertyChanged;
    }
}