using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TioUi.Demo.Pages;

public partial class TimeOnlyRangePickerPage : UserControl
{
    public TimeOnlyRangePickerPage()
    {
        InitializeComponent();
        DataContext = new TimeOnlyRangePickerDemoViewModel();
    }
}

public partial class TimeOnlyRangePickerDemoViewModel : ObservableObject
{
    [ObservableProperty] private TimeOnly? _startTime;
    [ObservableProperty] private TimeOnly? _endTime;

    public TimeOnlyRangePickerDemoViewModel()
    {
        StartTime = new TimeOnly(8, 21, 0);
        EndTime = new TimeOnly(18, 22, 0);
    }
}
