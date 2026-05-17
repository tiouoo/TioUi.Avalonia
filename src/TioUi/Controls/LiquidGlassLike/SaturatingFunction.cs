using System;
using System.Runtime.CompilerServices;

namespace TioUi.Controls;

/// <summary>
/// 饱和函数: y = kx / (x - b), x ≥ 0, b ≤ 0.
/// <br/>
/// k 是 <see cref="Limit"/>。当 x 增加时，y 趋近于 k。
/// <br/>
/// b 是 <see cref="Growth"/>。b 越大，增长越快。
/// </summary>
public class SaturatingFunction
{
    private double _k = 1;
    private double _b = -1;

    private double F(double x) => _k * x / (x - _b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Calculate(double x)
    {
#if DEBUG
        if (x < 0)
            throw new InvalidOperationException("x must be >= 0");
#endif
        if (_b == 0)
            return _k;

        if (x == 0)
            return 0;

        return F(x);
    }

    /// <summary>
    /// 限制值（渐近线）
    /// </summary>
    public double Limit
    {
        get => _k;
        set => _k = value;
    }

    /// <summary>
    /// 增长速率（必须小于等于0）
    /// </summary>
    public double Growth
    {
        get => _b;
        set
        {
#if DEBUG
            if (value > 0)
                throw new InvalidOperationException("Growth must be <= 0");
#endif
            _b = value;
        }
    }
}
