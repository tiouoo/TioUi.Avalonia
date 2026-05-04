namespace TioUi.Controls;

internal class RangePickerStatus
{
    public Status Current { get; private set; } = Status.None;
    public Status Previous { get; private set; } = Status.None;

    public void Push(Status status)
    {
        Previous = Current;
        Current = status;
    }

    public void Reset()
    {
        Current = Status.None;
        Previous = Status.None;
    }
}
