using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;
using TioUi.Common.Converter;
using TioUi.Shared;

namespace TioUi.Common.Helpers;

public static class ThemeHelper
{
    public static void SetThemeColor(Color accentColor)
    {
        var app = Application.Current;
        if (app?.Resources == null) return;

        app.Resources["SystemAccentColor"] = accentColor;

        app.Resources["SystemAccentColorLight1"] =
            ColorLightenConverter.Lighten15.Convert(accentColor, typeof(Color), null, null);
        app.Resources["SystemAccentColorLight2"] =
            ColorLightenConverter.Lighten30.Convert(accentColor, typeof(Color), null, null);
        app.Resources["SystemAccentColorLight3"] =
            ColorLightenConverter.Lighten45.Convert(accentColor, typeof(Color), null, null);

        app.Resources["SystemAccentColorDark1"] =
            ColorDarkenConverter.Darken15.Convert(accentColor, typeof(Color), null, null);
        app.Resources["SystemAccentColorDark2"] =
            ColorDarkenConverter.Darken30.Convert(accentColor, typeof(Color), null, null);
        app.Resources["SystemAccentColorDark3"] =
            ColorDarkenConverter.Darken45.Convert(accentColor, typeof(Color), null, null);
    }

    public static void SetThemeColor(string hexColor)
    {
        if (Color.TryParse(hexColor, out var color))
        {
            SetThemeColor(color);
        }
    }
    
    public static void ToggleTheme(Shared.Theme theme)
    {
        if (theme == Shared.Theme.Light)
            Application.Current.RequestedThemeVariant = ThemeVariant.Light;
        else if (theme == Shared.Theme.Dark)
            Application.Current.RequestedThemeVariant = ThemeVariant.Dark;
        else if (theme == Shared.Theme.Mirage)
            Application.Current.RequestedThemeVariant = Themes.Mirage;
        else if (theme == Shared.Theme.System)
            Application.Current.RequestedThemeVariant = ThemeVariant.Default;
    }
}