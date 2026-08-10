// Ported from SmoothScroll.Avalonia (https://github.com/zxbmmmmmmmmm/SmoothScroll.Avalonia) - MIT License.
namespace TioUi.Controls;

/// <summary>
/// Defines constants that specify the orientation of content scrolling in a <see cref = "ScrollView"/>.
/// </summary>
[Flags]
public enum ScrollContentOrientation
{
    None = 0,
    Vertical = 1,
    Horizontal = 2,
    Both = Vertical | Horizontal
}
