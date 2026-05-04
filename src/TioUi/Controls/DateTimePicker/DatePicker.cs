using System.Globalization;
using Avalonia;
using Avalonia.Controls.Metadata;

namespace TioUi.Controls;

[TemplatePart(PART_Popup, typeof(Avalonia.Controls.Primitives.Popup))]
[TemplatePart(PART_TextBox, typeof(Avalonia.Controls.TextBox))]
[TemplatePart(PART_Calendar, typeof(DatePickerCalendarView))]
public class DatePicker : DatePickerBase<DateTime>
{
    public static readonly StyledProperty<DateTimeKind> DefaultDateKindProperty =
        AvaloniaProperty.Register<DatePicker, DateTimeKind>(
            nameof(DefaultDateKind), DateTimeKind.Unspecified);

    protected override Type StyleKeyOverride => typeof(DatePickerBase);

    public DateTimeKind DefaultDateKind
    {
        get => GetValue(DefaultDateKindProperty);
        set => SetValue(DefaultDateKindProperty, value);
    }

    protected override DateOnly? ToDateOnly(DateTime? value)
        => value.HasValue ? DateOnly.FromDateTime(value.Value) : null;

    protected override DateTime FromDateOnly(DateOnly date)
        => DateTime.SpecifyKind(date.ToDateTime(TimeOnly.MinValue), DefaultDateKind);

    protected override DateTime? Parse(string? text, string? format)
    {
        if (string.IsNullOrWhiteSpace(text)) return null;
        if (string.IsNullOrWhiteSpace(format))
        {
            return DateTime.TryParse(text, out var result)
                ? DateTime.SpecifyKind(result, DefaultDateKind)
                : null;
        }
        return DateTime.TryParseExact(text, format, CultureInfo.CurrentUICulture, DateTimeStyles.None, out var date)
            ? DateTime.SpecifyKind(date, DefaultDateKind)
            : null;
    }

    protected override string? Format(DateTime? value, string? format)
        => value?.ToString(format ?? DEFAULT_DATE_DISPLAY_FORMAT);
}