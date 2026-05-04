using System.Globalization;
using Avalonia.Controls.Metadata;

namespace TioUi.Controls;

[TemplatePart(PART_Popup, typeof(Avalonia.Controls.Primitives.Popup))]
[TemplatePart(PART_StartCalendar, typeof(DatePickerCalendarView))]
[TemplatePart(PART_EndCalendar, typeof(DatePickerCalendarView))]
[TemplatePart(PART_StartTextBox, typeof(Avalonia.Controls.TextBox))]
[TemplatePart(PART_EndTextBox, typeof(Avalonia.Controls.TextBox))]
public class DateOnlyRangePicker : DateRangePickerBase<DateOnly>
{
    protected override Type StyleKeyOverride => typeof(DateRangePickerBase);

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
