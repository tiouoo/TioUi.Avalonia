// Ported from SmoothScroll.Avalonia (https://github.com/zxbmmmmmmmmm/SmoothScroll.Avalonia) - MIT License.
using Avalonia;

namespace TioUi.Controls.SmoothScroll.Interaction;

public sealed class InteractionTrackerValuesChangedArgs
{
    internal InteractionTrackerValuesChangedArgs(Vector3D position, double scale, int requestId)
    {
        Position = position;
        Scale = scale;
        RequestId = requestId;
    }

    public Vector3D Position { get; }

    public int RequestId { get; }

    public double Scale { get; }
}
