using Avalonia;
using Avalonia.Controls;

namespace TioUi.Controls;

public class TioExpander : Expander
{
    public static readonly StyledProperty<Thickness> IconMarginProperty =
        AvaloniaProperty.Register<TioExpander, Thickness>(nameof(IconMargin), new Thickness(10, 0, 0, 0));

    public Thickness IconMargin
    {
        get => GetValue(IconMarginProperty);
        set => SetValue(IconMarginProperty, value);
    }

    public static readonly StyledProperty<Thickness> HeaderPaddingProperty =
        AvaloniaProperty.Register<TioExpander, Thickness>(nameof(HeaderPadding), new Thickness(0));

    public Thickness HeaderPadding
    {
        get => GetValue(HeaderPaddingProperty);
        set => SetValue(HeaderPaddingProperty, value);
    }

    public static readonly StyledProperty<Thickness> HeaderMarginProperty =
        AvaloniaProperty.Register<TioExpander, Thickness>(nameof(HeaderMargin), new Thickness(0));

    public Thickness HeaderMargin
    {
        get => GetValue(HeaderMarginProperty);
        set => SetValue(HeaderMarginProperty, value);
    }
    
    public static readonly StyledProperty<double> IconWidthProperty =
        AvaloniaProperty.Register<TioExpander, double>(nameof(IconWidth), 10.0);

    public double IconWidth
    {
        get => GetValue(IconWidthProperty);
        set => SetValue(IconWidthProperty, value);
    }

    public static readonly StyledProperty<double> IconHeightProperty =
        AvaloniaProperty.Register<TioExpander, double>(nameof(IconHeight), 10.0);

    public double IconHeight
    {
        get => GetValue(IconHeightProperty);
        set => SetValue(IconHeightProperty, value);
    }

    public static readonly StyledProperty<double> HeaderHeightProperty =
        AvaloniaProperty.Register<TioExpander, double>(nameof(HeaderHeight), 32.0);

    public double HeaderHeight
    {
        get => GetValue(HeaderHeightProperty);
        set => SetValue(HeaderHeightProperty, value);
    }

    public static readonly DirectProperty<TioExpander, Thickness> UpMarginProperty =
        AvaloniaProperty.RegisterDirect<TioExpander, Thickness>(nameof(UpMargin), o => o.UpMargin);

    public static readonly DirectProperty<TioExpander, Thickness> DownMarginProperty =
        AvaloniaProperty.RegisterDirect<TioExpander, Thickness>(nameof(DownMargin), o => o.DownMargin);

    public static readonly DirectProperty<TioExpander, Thickness> LeftMarginProperty =
        AvaloniaProperty.RegisterDirect<TioExpander, Thickness>(nameof(LeftMargin), o => o.LeftMargin);

    public static readonly DirectProperty<TioExpander, Thickness> RightMarginProperty =
        AvaloniaProperty.RegisterDirect<TioExpander, Thickness>(nameof(RightMargin), o => o.RightMargin);

    private Thickness _upMargin;
    private Thickness _downMargin;
    private Thickness _leftMargin;
    private Thickness _rightMargin;

    public Thickness UpMargin => _upMargin;
    public Thickness DownMargin => _downMargin;
    public Thickness LeftMargin => _leftMargin;
    public Thickness RightMargin => _rightMargin;

    static TioExpander()
    {
        HeaderHeightProperty.Changed.AddClassHandler<TioExpander>((x, e) => x.UpdateAllMargins());
    }

    public TioExpander()
    {
        UpdateAllMargins();
    }

    private void UpdateAllMargins()
    {
        double h = HeaderHeight;
        SetAndRaise(UpMarginProperty, ref _upMargin, new Thickness(0, 0, 0, h));
        SetAndRaise(DownMarginProperty, ref _downMargin, new Thickness(0, h, 0, 0));
        SetAndRaise(LeftMarginProperty, ref _leftMargin, new Thickness(0, 0, h, 0));
        SetAndRaise(RightMarginProperty, ref _rightMargin, new Thickness(h, 0, 0, 0));
    }
}