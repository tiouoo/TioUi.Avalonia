using Avalonia.Interactivity;

namespace TioUi.Controls;

public class DatePickerCalendarYearButtonEventArgs : RoutedEventArgs
{
    /// <inheritdoc />
    internal DatePickerCalendarYearButtonEventArgs(DatePickerCalendarViewMode mode, DatePickerCalendarContext context)
    {
        Context = context;
        Mode = mode;
    }

    internal DatePickerCalendarContext Context { get; }
    internal DatePickerCalendarViewMode Mode { get; }
}