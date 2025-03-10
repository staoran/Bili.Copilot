﻿// Copyright (c) Bili Copilot. All rights reserved.

using System.Collections.Generic;
using Microsoft.Graphics.Canvas;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace Bili.Copilot.App.Controls.Base;

/// <summary>
/// 气泡发生器.
/// </summary>
public partial class BubbleView
{
    /// <summary>
    /// <see cref="IsBubbing"/>的依赖属性.
    /// </summary>
    public static readonly DependencyProperty IsBubbingProperty =
        DependencyProperty.Register(nameof(IsBubbing), typeof(bool), typeof(BubbleView), new PropertyMetadata(false, (s, a) =>
        {
            if (a.NewValue != a.OldValue)
            {
                if (s is BubbleView sender)
                {
                    if (a.NewValue is true)
                    {
                        sender.ShowBubbles();
                        sender.SetValue(IsBubbingProperty, false);
                    }
                }
            }
        }));

    private readonly long _foregroundPropertyChangedToken;

    private Rectangle _bubbleHost;
    private Color _foregroundColor;

    private Compositor _compositor;
    private Visual _hostVisual;
    private ContainerVisual _bubblesVisual;

    private CanvasDevice _canvasDevice;
    private CompositionGraphicsDevice _graphicsDevice;

    private List<Bubble> _bubbles;
    private bool _isLoaded;
    private bool _isApplied;

    /// <summary>
    /// 当设置IsBubbing = true时，触发ShowBubbles，并将IsBubbing设置为false，等待下次设置IsBubbing = true.
    /// </summary>
    public bool IsBubbing
    {
        get => (bool)GetValue(IsBubbingProperty);
        set => SetValue(IsBubbingProperty, value);
    }
}
