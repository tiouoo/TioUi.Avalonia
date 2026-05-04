using System.Globalization;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using TioUi.Common.Helpers;

namespace TioUi.Controls;

[TemplatePart(PART_Button, typeof(Button))]
[TemplatePart(PART_Popup, typeof(Popup))]
[TemplatePart(PART_TextBox, typeof(TextBox))]
[TemplatePart(PART_Calendar, typeof(DatePickerCalendarView))]
[TemplatePart(PART_TimePicker, typeof(TimePickerPresenter))]
public class DateTimePicker : DatePickerBase
{
    public const string PART_Button = "PART_Button";
    public const string PART_Popup = "PART_Popup";
    public const string PART_TextBox = "PART_TextBox";
    public const string PART_Calendar = "PART_Calendar";
    public const string PART_TimePicker = "PART_TimePicker";

    public static readonly StyledProperty<DateTime?> SelectedDateProperty =
        AvaloniaProperty.Register<DateTimePicker, DateTime?>(
            nameof(SelectedDate), defaultBindingMode: BindingMode.TwoWay);

    public static readonly StyledProperty<string?> PlaceholderTextProperty =
        AvaloniaProperty.Register<DateTimePicker, string?>(
            nameof(PlaceholderText));

    public static readonly StyledProperty<string> PanelFormatProperty = AvaloniaProperty.Register<TimePicker, string>(
        nameof(PanelFormat), "HH mm ss");

    public static readonly StyledProperty<bool> NeedConfirmationProperty = AvaloniaProperty.Register<TimePicker, bool>(
        nameof(NeedConfirmation));

    public static readonly StyledProperty<DateTimeKind> DefaultDateKindProperty =
        AvaloniaProperty.Register<DateTimePicker, DateTimeKind>(
            nameof(DefaultDateKind), DateTimeKind.Unspecified);

    private Button? _button;
    private DatePickerCalendarView? _calendar;

    private bool _fromText = false;

    private bool _isFocused;
    private Popup? _popup;
    private TextBox? _textBox;
    private TimePickerPresenter? _timePickerPresenter;

    static DateTimePicker()
    {
        FocusableProperty.OverrideDefaultValue<DateTimePicker>(true);
        DisplayFormatProperty.OverrideDefaultValue<DateTimePicker>(CultureInfo.InvariantCulture.DateTimeFormat
            .FullDateTimePattern);
        SelectedDateProperty.Changed.AddClassHandler<DateTimePicker, DateTime?>((picker, args) =>
            picker.OnSelectionChanged(args));
    }

    public DateTime? SelectedDate
    {
        get => GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }

    public string? PlaceholderText
    {
        get => GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty, value);
    }

    public string PanelFormat
    {
        get => GetValue(PanelFormatProperty);
        set => SetValue(PanelFormatProperty, value);
    }

    public bool NeedConfirmation
    {
        get => GetValue(NeedConfirmationProperty);
        set => SetValue(NeedConfirmationProperty, value);
    }

    public DateTimeKind DefaultDateKind
    {
        get => GetValue(DefaultDateKindProperty);
        set => SetValue(DefaultDateKindProperty, value);
    }

    private void OnSelectionChanged(AvaloniaPropertyChangedEventArgs<DateTime?> args)
    {
        if (_fromText) return;
        SyncSelectedDateToText(args.NewValue.Value);
    }

    private void SyncSelectedDateToText(DateTime? date)
    {
        if (date is null)
        {
            _textBox?.SetValue(TextBox.TextProperty, null);
            _calendar?.ClearSelection();
            _timePickerPresenter?.SyncTime(null);
        }
        else
        {
            _textBox?.SetValue(TextBox.TextProperty,
                date.Value.ToString(DisplayFormat ?? CultureInfo.InvariantCulture.DateTimeFormat.FullDateTimePattern));
            var selectedDate = date.ToDateOnly();
            _calendar?.MarkDates(selectedDate, selectedDate);
            _timePickerPresenter?.SyncTime(date.Value.ToTimeOnly());
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        GotFocusEvent.RemoveHandler(OnTextBoxGetFocus, _textBox);
        TextBox.TextChangedEvent.RemoveHandler(OnTextChanged, _textBox);
        Button.ClickEvent.RemoveHandler(OnButtonClick, _button);
        DatePickerCalendarView.DateSelectedEvent.RemoveHandler(OnDateSelected, _calendar);
        TimePickerPresenter.SelectedTimeChangedEvent.RemoveHandler(OnTimeSelectedChanged, _timePickerPresenter);
        _button = e.NameScope.Find<Button>(PART_Button);
        _popup = e.NameScope.Find<Popup>(PART_Popup);
        _textBox = e.NameScope.Find<TextBox>(PART_TextBox);
        _calendar = e.NameScope.Find<DatePickerCalendarView>(PART_Calendar);
        _timePickerPresenter = e.NameScope.Find<TimePickerPresenter>(PART_TimePicker);
        Button.ClickEvent.AddHandler(OnButtonClick, RoutingStrategies.Bubble, true, _button);
        GotFocusEvent.AddHandler(OnTextBoxGetFocus, _textBox);
        TextBox.TextChangedEvent.AddHandler(OnTextChanged, _textBox);
        DatePickerCalendarView.DateSelectedEvent.AddHandler(OnDateSelected, RoutingStrategies.Bubble, true, _calendar);
        TimePickerPresenter.SelectedTimeChangedEvent.AddHandler(OnTimeSelectedChanged, _timePickerPresenter);
        SyncSelectedDateToText(SelectedDate);
    }

    private void OnDateSelected(object? sender, DatePickerCalendarDayButtonEventArgs e)
    {
        if (SelectedDate is null)
        {
            if (e.Date is null) return;
            var date = e.Date.Value;
            var time = DateTime.Now.ToTimeOnly();
            SetCurrentValue(SelectedDateProperty, DateTime.SpecifyKind(date.ToDateTime(time), DefaultDateKind));
        }
        else
        {
            var selectedDate = SelectedDate;
            if (e.Date is null) return;
            var date = e.Date.Value;
            SetCurrentValue(SelectedDateProperty, DateTime.SpecifyKind(date.ToDateTime(selectedDate.Value.ToTimeOnly()), DefaultDateKind));
        }
    }

    private void OnTimeSelectedChanged(object? sender, TimeChangedEventArgs e)
    {
        if (SelectedDate is null)
        {
            if (e.NewTime is null) return;
            var time = e.NewTime.Value;
            SetCurrentValue(SelectedDateProperty, DateTime.SpecifyKind(DateTime.Today.ToDateOnly().ToDateTime(time), DefaultDateKind));
        }
        else
        {
            var selectedDate = SelectedDate;
            if (e.NewTime is null) return;
            var time = e.NewTime.Value;
            SetCurrentValue(SelectedDateProperty, DateTime.SpecifyKind(selectedDate.Value.ToDateOnly().ToDateTime(time), DefaultDateKind));
        }
    }

    private void OnButtonClick(object? sender, RoutedEventArgs e)
    {
        if (IsFocused)
        {
            SetCurrentValue(IsDropdownOpenProperty, !IsDropdownOpen);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        SetSelectedDate(true);
    }

    private void SetSelectedDate(bool fromText = false)
    {
        var temp = _fromText;
        _fromText = fromText;
        if (string.IsNullOrEmpty(_textBox?.Text))
        {
            SetCurrentValue(SelectedDateProperty, null);
            _calendar?.ClearSelection();
            _timePickerPresenter?.SyncTime(null);
        }
        else if (DisplayFormat is null || DisplayFormat.Length == 0)
        {
            if (DateTime.TryParse(_textBox?.Text, out var defaultTime))
            {
                SetCurrentValue(SelectedDateProperty, DateTime.SpecifyKind(defaultTime, DefaultDateKind));
                _calendar?.MarkDates(defaultTime.ToDateOnly(), defaultTime.ToDateOnly());
                _timePickerPresenter?.SyncTime(defaultTime.ToTimeOnly());
            }
        }
        else
        {
            CommitInput(!fromText);
        }

        _fromText = temp;
    }

    private void OnTextBoxGetFocus(object? sender, RoutedEventArgs e)
    {
        if (_calendar is not null)
        {
            var date = SelectedDate ?? DateTime.Today;
            _calendar.ContextDate = _calendar.ContextDate.With(date.Year, date.Month);
            _calendar.UpdateDayButtons();
            _timePickerPresenter?.SyncTime(date.ToTimeOnly());
        }

        SetCurrentValue(IsDropdownOpenProperty, true);
    }

    protected void OnGotFocus(RoutedEventArgs e)
    {
        FocusChanged(IsKeyboardFocusWithin);
    }

    protected void OnLostFocus(RoutedEventArgs e)
    {
        FocusChanged(IsKeyboardFocusWithin);
        var top = TopLevel.GetTopLevel(this);
        var element = top?.FocusManager?.GetFocusedElement();
        if (element is Visual v && _popup?.IsInsidePopup(v) == true)
        {
            return;
        }

        if (Equals(element, _textBox))
        {
            return;
        }

        CommitInput(true);
        SetCurrentValue(IsDropdownOpenProperty, false);
    }

    private void FocusChanged(bool hasFocus)
    {
        bool wasFocused = _isFocused;
        _isFocused = hasFocus;

        if (hasFocus)
        {
            if (!wasFocused && _textBox != null)
            {
                _textBox.Focus();
            }
        }
    }

    private void CommitInput(bool clearWhenInvalid)
    {
        if (DateTime.TryParseExact(_textBox?.Text, DisplayFormat, CultureInfo.CurrentUICulture, DateTimeStyles.None,
                out var date))
        {
            SetCurrentValue(SelectedDateProperty, DateTime.SpecifyKind(date, DefaultDateKind));
            if (_calendar is not null)
            {
                _calendar.ContextDate = _calendar.ContextDate.With(date.Year, date.Month);
                _calendar.UpdateDayButtons();
            }

            _calendar?.MarkDates(date.ToDateOnly(), date.ToDateOnly());
            _timePickerPresenter?.SyncTime(date.ToTimeOnly());
        }
        else
        {
            SetCurrentValue(SelectedDateProperty, null);
            if (clearWhenInvalid) _textBox?.SetValue(TextBox.TextProperty, null);
            _calendar?.ClearSelection();
            _timePickerPresenter?.SyncTime(null);
        }
    }


    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            SetCurrentValue(IsDropdownOpenProperty, false);
            e.Handled = true;
            return;
        }

        if (e.Key == Key.Down)
        {
            SetCurrentValue(IsDropdownOpenProperty, true);
            e.Handled = true;
            return;
        }

        if (e.Key == Key.Tab)
        {
            SetCurrentValue(IsDropdownOpenProperty, false);
            return;
        }

        base.OnKeyDown(e);
    }

    public void Clear()
    {
        SetCurrentValue(SelectedDateProperty, null);
    }
}