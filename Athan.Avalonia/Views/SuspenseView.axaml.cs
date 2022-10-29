using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Athan.Avalonia.Views;

internal sealed partial class SuspenseView : UserControl
{
    public static readonly StyledProperty<bool> ConditionProperty =
        AvaloniaProperty.Register<SuspenseView, bool>(nameof(Condition));

    public static readonly StyledProperty<Control> FallbackProperty =
        AvaloniaProperty.Register<SuspenseView, Control>(nameof(Fallback));

    public static readonly StyledProperty<Control> ActiveProperty =
        AvaloniaProperty.Register<SuspenseView, Control>(nameof(Active));

    public bool Condition
    {
        get => GetValue(ConditionProperty);
        set => SetValue(ConditionProperty, value);
    }

    public Control Fallback
    {
        get => GetValue(FallbackProperty);
        set => SetValue(FallbackProperty, value);
    }

    public Control Active
    {
        get => GetValue(ActiveProperty);
        set => SetValue(ActiveProperty, value);
    }

    public SuspenseView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Content = Condition ? Active : Fallback;
    }

    protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == ConditionProperty)
        {
            Content = Condition ? Active : Fallback;
        }
    }
}