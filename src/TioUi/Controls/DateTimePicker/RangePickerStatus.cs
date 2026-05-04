namespace TioUi.Controls;

internal class RangePickerStatus
{
    public Status StartStatus { get; set; } = Status.Idle;
    public Status EndStatus { get; set; } = Status.Idle;

    public void Reset()
    {
        StartStatus = Status.Idle;
        EndStatus = Status.Idle;
    }

    public bool IsIdle => StartStatus == Status.Idle && EndStatus == Status.Idle;
    public bool IsSelecting => StartStatus == Status.Selecting || EndStatus == Status.Selecting;
    public bool IsSelected => StartStatus == Status.Selected && EndStatus == Status.Selected;
}
