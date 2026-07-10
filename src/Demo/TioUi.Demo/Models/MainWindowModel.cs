using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Styling;
using TioUi.Common.Language;

namespace TioUi.Demo.Models;

public sealed class MainWindowModel : INotifyPropertyChanged
{
    public DemoView DemoView { get; } = new();
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }

    public void ToggleTheme(object? theme)
    {
        var t = theme as string;

        if (Application.Current != null && t != null)
        {
            Application.Current.RequestedThemeVariant =
                Application.Current.ActualThemeVariant == ThemeVariant.Dark
                    ? ThemeVariant.Light
                    : ThemeVariant.Dark;
        }

        if (t == "a")
            Application.Current!.RequestedThemeVariant = ThemeVariant.Default;
        else if (t == "l")
            Application.Current!.RequestedThemeVariant = ThemeVariant.Light;
        else if (t == "d")
            Application.Current!.RequestedThemeVariant = ThemeVariant.Dark;
    }


    public void ToggleLang(object? l)
    {
        var lang = l as string;
        if (lang == null) return;

        if (lang == "c")
            LangManager.SetLanguage(Languages.zh_cn);
        else if (lang == "e")
            LangManager.SetLanguage(Languages.en_us);
    }
}