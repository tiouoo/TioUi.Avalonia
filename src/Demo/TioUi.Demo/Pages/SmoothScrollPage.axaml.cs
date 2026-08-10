using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TioUi.Demo.Pages;

public partial class SmoothScrollPage : UserControl
{
    public SmoothScrollPage()
    {
        InitializeComponent();
    }

    private void ZoomInButton_Click(object? sender, RoutedEventArgs e)
    {
        ZoomScrollView.ZoomTo(ZoomScrollView.ZoomFactor * 1.2);
    }

    private void ZoomOutButton_Click(object? sender, RoutedEventArgs e)
    {
        ZoomScrollView.ZoomTo(ZoomScrollView.ZoomFactor / 1.2);
    }
}
