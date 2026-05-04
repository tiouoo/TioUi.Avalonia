using Avalonia.Interactivity;

namespace TioUi.Controls;

public class TimeChangedEventArgs : RoutedEventArgs
{
    public TimeChangedEventArgs(TimeOnly? oldTime, TimeOnly? newTime)
    {
        this.OldTime = oldTime;
        this.NewTime = newTime;
    }

    public TimeOnly? OldTime { get; }

    public TimeOnly? NewTime { get; }
}