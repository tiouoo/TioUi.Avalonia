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
    private bool _isDragging;

    static LiquidGlassLikeDecorator()
    {
        AffectsRender<LiquidGlassLikeDecorator>(ChildProperty);
    }

    public LiquidGlassLikeDecorator()
    {
        _controller = new LiquidGlassLikeTransformController(this);
        RenderTransform = _controller.Transform;
        RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
        
        // 使用隧道事件（Tunneling）在事件到达子控件之前捕获
        AddHandler(PointerPressedEvent, OnPointerPressedTunnel, handledEventsToo: true, routes: Avalonia.Interactivity.RoutingStrategies.Tunnel);
        AddHandler(PointerMovedEvent, OnPointerMovedTunnel, handledEventsToo: true, routes: Avalonia.Interactivity.RoutingStrategies.Tunnel);
        AddHandler(PointerReleasedEvent, OnPointerReleasedTunnel, handledEventsToo: true, routes: Avalonia.Interactivity.RoutingStrategies.Tunnel);
    }

    private void OnPointerPressedTunnel(object? sender, PointerPressedEventArgs e)
    {
        var point = e.GetCurrentPoint(this);
        if (point.Properties.IsLeftButtonPressed)
        {
            _isMouseDown = true;
            _isDragging = false;
            _mouseDownPosition = point.Position;
            // 不在这里捕获指针，让子控件可以正常响应点击
        }
    }

    private void OnPointerMovedTunnel(object? sender, PointerEventArgs e)
    {
        if (_isMouseDown)
        {
            var currentPosition = e.GetPosition(this);
            var dragDelta = currentPosition - _mouseDownPosition;
            
            // 检测是否开始拖动（移动超过阈值）
            if (!_isDragging && (Math.Abs(dragDelta.X) > 3 || Math.Abs(dragDelta.Y) > 3))
            {
                _isDragging = true;
                // 开始拖动时才捕获指针
                e.Pointer.Capture(this);
            }

            if (_isDragging)
            {
                _controller.DragDelta = dragDelta;
                // 标记事件已处理，防止子控件响应拖动
                e.Handled = true;
            }
        }
    }

    private void OnPointerReleasedTunnel(object? sender, PointerReleasedEventArgs e)
    {
        if (_isMouseDown)
        {
            var point = e.GetCurrentPoint(this);
            if (!point.Properties.IsLeftButtonPressed)
            {
                if (_isDragging)
                {
                    // 如果正在拖动，标记事件已处理，防止触发子控件的点击
                    e.Handled = true;
                }
                
                e.Pointer.Capture(null);
                _isMouseDown = false;
                _isDragging = false;
                _controller.Reset();
            }
        }
    }

    protected override void OnPointerCaptureLost(PointerCaptureLostEventArgs e)
    {
        if (_isMouseDown)
        {
            _isMouseDown = false;
            _isDragging = false;
            _controller.Reset();
        }
        
        base.OnPointerCaptureLost(e);
    }
}
