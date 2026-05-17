using System;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;

namespace TioUi.Controls;

public class LiquidLikeTransformController
{
    private readonly Control _target;
    private readonly TransformGroup _transformGroup;
    private readonly ScaleTransform _scaleTransform = new(1, 1);
    private readonly TranslateTransform _translateTransform = new(0, 0);
    private readonly LiquidLikeStretchCalculator _stretchCalculator = new();
    
    private Vector _dragDelta;
    private DispatcherTimer? _resetTimer;
    private double _animationProgress;
    private double _startScaleX, _startScaleY, _startTranslateX, _startTranslateY;

    public LiquidLikeTransformController(Control target)
    {
        _target = target;
        _target.SizeChanged += OnTargetSizeChanged;
        
        _transformGroup = new TransformGroup
        {
            Children = new Transforms
            {
                _scaleTransform,
                _translateTransform
            }
        };
        
        ResetAnimationDuration = TimeSpan.FromSeconds(0.382);
    }

    public Transform Transform => _transformGroup;

    public Vector DragDelta
    {
        get => _dragDelta;
        set
        {
            _dragDelta = value;
            OnDragDeltaChanged();
        }
    }

    public TimeSpan ResetAnimationDuration { get; set; }

    public void Reset()
    {
        if (DragDelta.X == 0 && DragDelta.Y == 0)
            return;

        _dragDelta = new Vector(0, 0);
        
        _resetTimer?.Stop();
        
        _startScaleX = _scaleTransform.ScaleX;
        _startScaleY = _scaleTransform.ScaleY;
        _startTranslateX = _translateTransform.X;
        _startTranslateY = _translateTransform.Y;
        _animationProgress = 0;

        _resetTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(16)
        };
        _resetTimer.Tick += OnResetTimerTick;
        _resetTimer.Start();
    }

    private void OnResetTimerTick(object? sender, EventArgs e)
    {
        _animationProgress += 16.0 / ResetAnimationDuration.TotalMilliseconds;
        
        if (_animationProgress >= 1.0)
        {
            _scaleTransform.ScaleX = 1;
            _scaleTransform.ScaleY = 1;
            _translateTransform.X = 0;
            _translateTransform.Y = 0;
            _resetTimer?.Stop();
            _resetTimer = null;
            return;
        }

        var easing = new BackEaseOut();
        var easedProgress = easing.Ease(_animationProgress);

        _scaleTransform.ScaleX = _startScaleX + (1.0 - _startScaleX) * easedProgress;
        _scaleTransform.ScaleY = _startScaleY + (1.0 - _startScaleY) * easedProgress;
        _translateTransform.X = _startTranslateX + (0.0 - _startTranslateX) * easedProgress;
        _translateTransform.Y = _startTranslateY + (0.0 - _startTranslateY) * easedProgress;
    }

    private void OnTargetSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        _stretchCalculator.OriginalSize = e.NewSize;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void OnDragDeltaChanged()
    {
        _stretchCalculator.DragDelta = DragDelta;
        _stretchCalculator.Calculate();
        RefreshScaleTransform();
        RefreshTranslateTransform();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RefreshScaleTransform()
    {
        if (_target.Bounds.Width > 0 && _target.Bounds.Height > 0)
        {
            _scaleTransform.ScaleX = 1 + _stretchCalculator.StretchX / _target.Bounds.Width;
            _scaleTransform.ScaleY = 1 + _stretchCalculator.StretchY / _target.Bounds.Height;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RefreshTranslateTransform()
    {
        _translateTransform.X = Math.Sign(DragDelta.X) * _stretchCalculator.OffsetX;
        _translateTransform.Y = Math.Sign(DragDelta.Y) * _stretchCalculator.OffsetY;
    }
}
