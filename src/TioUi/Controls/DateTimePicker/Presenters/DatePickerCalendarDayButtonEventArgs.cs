using Avalonia.Interactivity;

namespace TioUi.Controls;

public class DatePickerCalendarDayButtonEventArgs(DateOnly? date) : RoutedEventArgs
{
    public DateOnly? Date { get; private set; } = date;
}