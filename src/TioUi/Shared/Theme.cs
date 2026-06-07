using Avalonia.Styling;

namespace TioUi.Shared;

public static class Themes
{
    public static ThemeVariant Mirage { get; } = new("Mirage", ThemeVariant.Dark);
}

public enum Theme
{
    System,
    Light,
    Dark,
    Mirage
}