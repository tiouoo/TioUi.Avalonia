// Ported from SmoothScroll.Avalonia (https://github.com/zxbmmmmmmmmm/SmoothScroll.Avalonia) - MIT License.
namespace TioUi.Controls.SmoothScroll.Interaction;

public class InteractionTrackerRequestIgnoredArgs
{
    internal InteractionTrackerRequestIgnoredArgs(int requestId)
        => RequestId = requestId;

    public int RequestId { get; }
}
