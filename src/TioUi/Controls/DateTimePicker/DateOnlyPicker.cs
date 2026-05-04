using System.Globalization;
using Avalonia.Controls.Metadata;

namespace TioUi.Controls;

[TemplatePart(PART_Popup, typeof(Avalonia.Controls.Primitives.Popup))]
[TemplatePart(PART_TextBox, typeof(Avalonia.Controls.TextBox))]
[TemplatePart(PART_Calendar, typeof(DatePickerCalendarView))]
public class DateOnlyPicker : DatePickerBase<DateOnly>
{
    protected override Type StyleKeyOverride => typeof(DatePickerBase);

    protected override DateOnly? ToDateOnly(DateOnly? value) => value;

    protected override DateOnly FromDateOnly(DateOnly date) => date;

    protected override DateOnly? Parse(string? text, string? format)
    {
        if (string.IsNullOrWhiteSpace(text)) return null;
        if (string.IsNullOrWhiteSpace(format))
        {
            return DateOnly.TryParse(text, out var result) ? result : null;
        }
        return DateOnly.TryParseExact(text, format, CultureInfo.CurrentUICulture, DateTimeStyles.None, out var date)
            ? date
            : null;
    }

    protected override string? Format(DateOnly? value, string? format)
        => value?.ToString(format ?? DEFAULT_DATE_DISPLAY_FORMAT);
}
