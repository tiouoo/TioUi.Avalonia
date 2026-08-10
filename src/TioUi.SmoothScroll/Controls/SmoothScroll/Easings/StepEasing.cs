// Ported from SmoothScroll.Avalonia (https://github.com/zxbmmmmmmmmm/SmoothScroll.Avalonia) - MIT License.
using Avalonia.Animation.Easings;

namespace TioUi.Controls.Easings;


internal class StepEasing : Easing
{
    public override double Ease(double progress)
    {
        return progress < 0.5 ? 0 : 1;
    }
}
