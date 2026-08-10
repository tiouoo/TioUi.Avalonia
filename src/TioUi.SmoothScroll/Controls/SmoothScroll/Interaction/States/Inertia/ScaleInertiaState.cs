// Ported from SmoothScroll.Avalonia (https://github.com/zxbmmmmmmmmm/SmoothScroll.Avalonia) - MIT License.
using Avalonia;
using Avalonia.Utilities;

namespace TioUi.Controls.SmoothScroll.Interaction;

internal class ScaleInertiaState : InertiaState
{
    public ScaleInertiaState(
        ServerInteractionTracker interactionTracker,
        Point scaleOrigin,
        double scaleVelocity,
        int requestId) : base(interactionTracker, requestId)
    {
        Handler = new ScaleInertiaHandler(
            interactionTracker.Compositor,
            interactionTracker,
            scaleOrigin,
            scaleVelocity);
        EnterState();
    }
}
