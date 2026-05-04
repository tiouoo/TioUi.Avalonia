using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TioUi.Demo.Pages;

public partial class TimeOnlyPickerPage : UserControl
{
    public TimeOnlyPickerPage()
    {
        InitializeComponent();
        DataContext = new TimeOnlyPickerDemoViewModel();
    }
}

public partial class TimeOnlyPickerDemoViewModel : ObservableObject
{
    [ObservableProperty] private TimeOnly? _time;

    public TimeOnlyPickerDemoViewModel()
    {
        Time = new TimeOnly(12, 20, 0);
    }
}
