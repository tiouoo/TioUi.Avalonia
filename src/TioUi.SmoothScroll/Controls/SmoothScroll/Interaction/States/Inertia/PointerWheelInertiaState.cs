// Ported from SmoothScroll.Avalonia (https://github.com/zxbmmmmmmmmm/SmoothScroll.Avalonia) - MIT License.
using Avalonia;
using Avalonia.Utilities;

namespace TioUi.Controls.SmoothScroll.Interaction;

internal sealed class PointerWheelInertiaState : InertiaState
{

    public PointerWheelInertiaState(
        ServerInteractionTracker interactionTracker,
        Vector3D translationVelocities,
        int requestId) : base(interactionTracker, requestId)
    {
        Handler = new PointerWheelInertiaHandler(
            interactionTracker.Compositor,
            interactionTracker,
            translationVelocities);
        EnterState();
    }
}
