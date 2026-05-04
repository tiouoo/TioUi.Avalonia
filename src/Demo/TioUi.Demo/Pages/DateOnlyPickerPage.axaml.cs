using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TioUi.Demo.Pages;

public partial class DateOnlyPickerPage : UserControl
{
    public DateOnlyPickerPage()
    {
        InitializeComponent();
        DataContext = new DateOnlyPickerDemoViewModel();
    }
}

public partial class DateOnlyPickerDemoViewModel : ObservableObject
{
    [ObservableProperty] private DateOnly? _selectedDate;

    public DateOnlyPickerDemoViewModel()
    {
        SelectedDate = DateOnly.FromDateTime(DateTime.Today);
    }
}
