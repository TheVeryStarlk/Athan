using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Athan.UI.Features.Shell;

internal sealed partial class ItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate? HeaderTemplate { get; set; }

    public DataTemplate? FooterTemplate { get; set; }

    protected override DataTemplate? SelectTemplateCore(object item)
    {
        return item switch
        {
            HeaderViewModel => HeaderTemplate,
            FooterViewModel => FooterTemplate,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}