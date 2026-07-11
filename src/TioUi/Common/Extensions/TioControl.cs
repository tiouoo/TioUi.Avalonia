using Avalonia.Controls;
using Avalonia.VisualTree;
using TioUi.Controls;

namespace TioUi.Common.Extensions;

public static class TioControl
{
    public static TopLevel GetTopLevel(this Control control)
    {
        return TopLevel.GetTopLevel(control);
    }

    public static string? TryGetHostId(this Control control)
    {
        if (control.GetTopLevel() is TioWindow tioWindow)
            return tioWindow.HostId;

        if (control is TioView view)
            return view.HostId;
        var tioView = control.GetVisualAncestors().OfType<TioView>().FirstOrDefault();
        return tioView?.HostId;
    }
}