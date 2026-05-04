using Avalonia.Interactivity;

namespace TioUi.Controls;

public class DatePickerCalendarDayButtonEventArgs(DateTime? date) : RoutedEventArgs
{
    public DateTime? Date { get; private set; } = date;
}