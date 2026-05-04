using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TioUi.Demo.Pages;

public partial class DateOnlyRangePickerPage : UserControl
{
    public DateOnlyRangePickerPage()
    {
        InitializeComponent();
        DataContext = new DateOnlyRangePickerDemoViewModel();
    }
}

public partial class DateOnlyRangePickerDemoViewModel : ObservableObject
{
    [ObservableProperty] private DateOnly? _startDate;
    [ObservableProperty] private DateOnly? _endDate;

    public DateOnlyRangePickerDemoViewModel()
    {
        StartDate = DateOnly.FromDateTime(DateTime.Today);
        EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(7));
    }
}
