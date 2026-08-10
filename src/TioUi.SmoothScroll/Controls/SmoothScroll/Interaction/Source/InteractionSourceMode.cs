// Ported from SmoothScroll.Avalonia (https://github.com/zxbmmmmmmmmm/SmoothScroll.Avalonia) - MIT License.
using System;
using System.Collections.Generic;
using System.Text;

namespace TioUi.Controls.SmoothScroll.Interaction;

/// <summary>
/// Provides the various definitions for how a VisualInteractionSource will process interactions.
/// Options available for the enumeration are Disabled, EnabledWithInertia and EnabledWithoutInertia.
/// The InteractionSourceMode can be used to define the behavior for the X, Y and Scale Axis of a VisualInteractionSource.
/// </summary>
public enum InteractionSourceMode
{
    /// <summary>
    /// Interaction is disabled.
    /// </summary>
    Disabled = 0,

    /// <summary>
    /// Interaction is enabled with inertia.
    /// </summary>
    EnabledWithInertia = 1,

    /// <summary>
    /// Interaction is enabled without inertia.
    /// </summary>
    EnabledWithoutInertia = 2,
}
