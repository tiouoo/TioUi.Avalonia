using Avalonia.Interactivity;

namespace TioUi.Controls;

public class MessageClosedEventArgs(MessageCloseReason reason) : RoutedEventArgs
{
    public MessageCloseReason Reason { get; } = reason;
}
