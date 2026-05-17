using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace TioUi.Controls;

public class LiquidGlassLikeDecorator : Decorator
{
    private readonly LiquidGlassLikeTransformController _controller;
    private bool _isMouseDown;
    private Point _mouseDownPosition;

    static LiquidGlassLikeDecorator()
    {
        AffectsRender<LiquidGlassLikeDecorator>(ChildProperty);
    }

    public LiquidGlassLikeDecorator()
    {
        _controller = new LiquidGlassLikeTransformController(this);
        RenderTransform = _controller.Transform;
        RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        var point = e.GetCurrentPoint(this);
        if (point.Properties.IsLeftButtonPressed)
        {
            _isMouseDown = true;
            _mouseDownPosition = point.Position;
            e.Pointer.Capture(this);
        }
        
        base.OnPointerPressed(e);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        if (_isMouseDown)
        {
            var currentPosition = e.GetPosition(this);
            var dragDelta = currentPosition - _mouseDownPosition;
            
            if (Math.Abs(dragDelta.X) > 2 || Math.Abs(dragDelta.Y) > 2)
            {
                _controller.DragDelta = dragDelta;
                e.Handled = true;
            }
        }
        
        base.OnPointerMoved(e);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        if (_isMouseDown)
        {
            var point = e.GetCurrentPoint(this);
            if (!point.Properties.IsLeftButtonPressed)
            {
                e.Pointer.Capture(null);
                _isMouseDown = false;
                _controller.Reset();
            }
        }
        
        base.OnPointerReleased(e);
    }

    protected override void OnPointerCaptureLost(PointerCaptureLostEventArgs e)
    {
        if (_isMouseDown)
        {
            _isMouseDown = false;
            _controller.Reset();
        }
        
        base.OnPointerCaptureLost(e);
    }
}
