// Ported from SmoothScroll.Avalonia (https://github.com/zxbmmmmmmmmm/SmoothScroll.Avalonia) - MIT License.
using Avalonia;

namespace TioUi.Controls.SmoothScroll.Interaction;

internal interface IInteractionTrackerInertiaHandler
{
    Vector3D InitialVelocity { get; }
    Vector3D FinalPosition { get; }
    Vector3D FinalModifiedPosition { get; }
    double FinalModifiedScale { get; }

    void Start();
    void Stop();
}
