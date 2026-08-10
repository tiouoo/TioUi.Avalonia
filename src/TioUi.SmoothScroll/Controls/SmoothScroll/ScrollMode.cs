// Ported from SmoothScroll.Avalonia (https://github.com/zxbmmmmmmmmm/SmoothScroll.Avalonia) - MIT License.
namespace TioUi.Controls;

/// <summary>
/// Defines constants that specify scrolling behavior for the <see cref = "ScrollView"/> control.
/// </summary>
public enum ScrollMode
{
    /// <summary>
    /// Scrolling is enabled.
    /// </summary>
    Enabled,

    /// <summary>
    /// Scrolling is disabled.
    /// </summary>
    Disabled,

    /// <summary>
    /// Scrolling is enabled but behavior uses a "rails" manipulation mode.
    /// </summary>
    Auto
}
