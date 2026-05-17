using System;
using System.Runtime.CompilerServices;
using Avalonia;

namespace TioUi.Controls;

/// <summary>
/// 液态玻璃拉伸计算器
/// </summary>
public class LiquidLikeStretchCalculator
{
    public Size OriginalSize { get; set; }
    public Vector DragDelta { get; set; }

    public double StretchX { get; private set; }
    public double StretchY { get; private set; }

    public double OffsetX { get; private set; }
    public double OffsetY { get; private set; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Calculate()
    {
        double absDeltaX = Math.Abs(DragDelta.X);
        double absDeltaY = Math.Abs(DragDelta.Y);
        
        // 轴向拉伸 - 垂直方向收缩
        StretchX = AxialStretchingFunction.Calculate(absDeltaX) 
                   - VerticalShrinkFunction.Calculate(absDeltaY) * OriginalSize.Width;
        StretchY = AxialStretchingFunction.Calculate(absDeltaY) 
                   - VerticalShrinkFunction.Calculate(absDeltaX) * OriginalSize.Height;
        
        // 偏移量
        OffsetX = OffsetFunction.Calculate(absDeltaX);
        OffsetY = OffsetFunction.Calculate(absDeltaY);
    }

    /// <summary>
    /// 绝对拉伸函数
    /// </summary>
    private static readonly SaturatingFunction AxialStretchingFunction = new()
    {
        Limit = 16,
        Growth = -618
    };

    /// <summary>
    /// 相对收缩函数
    /// </summary>
    private static readonly SaturatingFunction VerticalShrinkFunction = new()
    {
        Limit = 0.382,
        Growth = -618
    };

    /// <summary>
    /// 绝对偏移函数
    /// </summary>
    private static readonly SaturatingFunction OffsetFunction = new()
    {
        Limit = 24,
        Growth = -618
    };
}
