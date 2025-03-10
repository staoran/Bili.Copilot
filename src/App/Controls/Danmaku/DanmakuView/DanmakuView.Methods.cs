﻿// Copyright (c) Bili Copilot. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Bili.Copilot.App.Extensions;
using Bili.Copilot.Models.Constants.App;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.UI;

namespace Bili.Copilot.App.Controls.Danmaku;

/// <summary>
/// 弹幕视图.
/// </summary>
public sealed partial class DanmakuView
{
    /// <summary>
    /// 添加直播滚动弹幕.
    /// </summary>
    /// <param name="text">参数.</param>
    /// <param name="own">是否自己发送的.</param>
    /// <param name="color">颜色.</param>
    public void AddLiveDanmaku(string text, bool own, Color? color)
    {
        if (color == null)
        {
            color = Colors.White;
        }

        var m = new DanmakuModel()
        {
            Text = text,
            Color = color.Value,
            Location = DanmakuLocation.Scroll,
            Size = 25,
        };

        var grid = CreateNewDanmuControl(m);
        if (own)
        {
            grid.BorderBrush = new SolidColorBrush(color.Value);
            grid.BorderThickness = new Thickness(1);
        }

        var r = GetScrollAvailableRow(grid);
        if (r == -1)
        {
            return;
        }

        Grid.SetRow(grid, r);
        grid.HorizontalAlignment = HorizontalAlignment.Left;
        grid.VerticalAlignment = VerticalAlignment.Center;
        _scrollContainer.Children.Add(grid);
        _scrollContainer.UpdateLayout();

        var moveTransform = new TranslateTransform
        {
            X = _rootGrid.ActualWidth,
        };
        grid.RenderTransform = moveTransform;

        // 创建动画
        var duration = new Duration(TimeSpan.FromSeconds(DanmakuDuration));
        var myDoubleAnimationX = new DoubleAnimation
        {
            Duration = duration,
        };

        // 创建故事版
        var moveStoryboard = new Storyboard
        {
            Duration = duration,
        };
        myDoubleAnimationX.To = -grid.ActualWidth;
        moveStoryboard.Children.Add(myDoubleAnimationX);
        Storyboard.SetTarget(myDoubleAnimationX, moveTransform);

        // 故事版加入动画
        Storyboard.SetTargetProperty(myDoubleAnimationX, "X");
        _scrollStoryList.Add(moveStoryboard);

        moveStoryboard.Completed += new EventHandler<object>((senders, obj) =>
        {
            _ = _scrollContainer.Children.Remove(grid);
            grid.Children.Clear();
            grid = null;
            _ = _scrollStoryList.Remove(moveStoryboard);
            moveStoryboard.Stop();
            moveStoryboard = null;
        });

        moveStoryboard.Begin();
    }

    /// <summary>
    /// 添加一条弹幕.
    /// </summary>
    /// <param name="m">弹幕实例.</param>
    /// <param name="isOwn">是否是自己所发.</param>
    public void AddScreenDanmaku(DanmakuModel m, bool isOwn)
    {
        m.IsSend = true;
        switch (m.Location)
        {
            case DanmakuLocation.Scroll:
            case DanmakuLocation.Top:
            case DanmakuLocation.Bottom:
                AddDanmakuInternal(m, isOwn);
                break;
            case DanmakuLocation.Position:
                AddPositionDanmaku(m);
                break;
            default:
                AddDanmakuInternal(m, isOwn);
                break;
        }
    }

    /// <summary>
    /// 添加定位弹幕.
    /// </summary>
    /// <param name="m">参数.</param>
    public void AddPositionDanmaku(DanmakuModel m)
    {
        var data = JsonSerializer.Deserialize<object[]>(m.Text);
        m.Text = data[4].ToString().Replace("/n", "\r\n");
        var danmaku = CreateNewDanmuControl(m);
        var danmakuFontFamily = data[^2].ToString();

        danmaku.Tag = m;
        danmaku.HorizontalAlignment = HorizontalAlignment.Left;
        danmaku.VerticalAlignment = VerticalAlignment.Center;

        double toX = 0;
        double toY = 0;

        double x = 0, y = 0;
        double dur = 0;

        if (data.Length > 7)
        {
            x = data[0].ToDouble();
            y = data[1].ToDouble();

            toX = data[7].ToDouble();
            toY = data[8].ToDouble();

            dur = data[10].ToDouble();
        }
        else
        {
            toX = data[0].ToDouble();
            toY = data[1].ToDouble();
        }

        if (toX < 1 && toY < 1)
        {
            toX = ActualWidth * toX;
            toY = ActualHeight * toY;
        }

        if (x < 1 && y < 1)
        {
            x = ActualWidth * x;
            y = ActualHeight * y;
        }

        if (data.Length >= 7)
        {
            var rotateZ = data[5].ToDouble();
            var rotateY = data[6].ToDouble();
            var projection = new PlaneProjection
            {
                RotationZ = -rotateZ,
                RotationY = rotateY,
            };
            danmaku.Projection = projection;
        }

        _canvas.Children.Add(danmaku);

        var dmDuration = data[3].ToDouble();
        var opacitys = data[2].ToString().Split('-');
        var opacityFrom = opacitys[0].ToDouble();
        var opacityTo = opacitys[1].ToDouble();

        // 创建故事版
        var moveStoryboard = new Storyboard();

        var duration = new Duration(TimeSpan.FromMilliseconds(dur));

        var myDoubleAnimationY = new DoubleAnimation
        {
            Duration = duration,
            From = y,
            To = toY,
        };
        Storyboard.SetTarget(myDoubleAnimationY, danmaku);
        Storyboard.SetTargetProperty(myDoubleAnimationY, "(Canvas.Top)");
        moveStoryboard.Children.Add(myDoubleAnimationY);

        var myDoubleAnimationX = new DoubleAnimation
        {
            Duration = duration,
            From = x,
            To = toX,
        };
        Storyboard.SetTarget(myDoubleAnimationX, danmaku);
        Storyboard.SetTargetProperty(myDoubleAnimationX, "(Canvas.Left)");
        moveStoryboard.Children.Add(myDoubleAnimationX);

        // 透明度动画
        var opacityAnimation = new DoubleAnimation()
        {
            Duration = new Duration(TimeSpan.FromSeconds(dmDuration)),
            From = opacityFrom,
            To = opacityTo,
        };

        Storyboard.SetTarget(opacityAnimation, danmaku);
        Storyboard.SetTargetProperty(opacityAnimation, "Opacity");
        moveStoryboard.Children.Add(opacityAnimation);

        _positionStoryList.Add(moveStoryboard);

        moveStoryboard.Completed += new EventHandler<object>((senders, obj) =>
        {
            _ = _canvas.Children.Remove(danmaku);
            _ = _positionStoryList.Remove(moveStoryboard);
        });

        moveStoryboard.Begin();
    }

    /// <summary>
    /// 暂停弹幕.
    /// </summary>
    public void PauseDanmaku()
    {
        foreach (var item in _topBottomStoryList.Concat(_scrollStoryList).Concat(_positionStoryList))
        {
            item.Pause();
        }
    }

    /// <summary>
    /// 继续弹幕.
    /// </summary>
    public void ResumeDanmaku()
    {
        foreach (var item in _topBottomStoryList.Concat(_scrollStoryList).Concat(_positionStoryList))
        {
            item.Resume();
        }
    }

    /// <summary>
    /// 移除指定弹幕.
    /// </summary>
    /// <param name="danmaku">参数.</param>
    public void Remove(DanmakuModel danmaku)
    {
        if (!_isApplyTemplate)
        {
            return;
        }

        switch (danmaku.Location)
        {
            case DanmakuLocation.Top:

                foreach (var item in _topContainer.Children)
                {
                    if ((((FrameworkElement)item).Tag as DanmakuModel) == danmaku)
                    {
                        _ = _topContainer.Children.Remove(item);
                    }
                }

                break;
            case DanmakuLocation.Bottom:
                foreach (var item in _bottomContainer.Children)
                {
                    if ((((FrameworkElement)item).Tag as DanmakuModel) == danmaku)
                    {
                        _ = _bottomContainer.Children.Remove(item);
                    }
                }

                break;
            case DanmakuLocation.Other:
                foreach (var item in _scrollContainer.Children)
                {
                    if ((((FrameworkElement)item).Tag as DanmakuModel) == danmaku)
                    {
                        _ = _scrollContainer.Children.Remove(item);
                    }
                }

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 清空弹幕.
    /// </summary>
    public void ClearAll()
    {
        if (!_isApplyTemplate)
        {
            return;
        }

        _topBottomStoryList.Clear();
        _scrollStoryList.Clear();
        _bottomContainer.Children.Clear();
        _topContainer.Children.Clear();
        _scrollContainer.Children.Clear();
    }

    /// <summary>
    /// 读取屏幕上的全部弹幕.
    /// </summary>
    /// <param name="danmakuLocation">类型.</param>
    /// <returns>弹幕列表.</returns>
    public List<DanmakuModel> GetDanmakus(DanmakuLocation? danmakuLocation = null)
    {
        if (!_isApplyTemplate)
        {
            return null;
        }

        var danmakus = new List<DanmakuModel>();
        if (danmakuLocation is null or DanmakuLocation.Top)
        {
            foreach (Grid item in _topContainer.Children)
            {
                danmakus.Add(item.Tag as DanmakuModel);
            }
        }

        if (danmakuLocation is null or DanmakuLocation.Bottom)
        {
            foreach (Grid item in _bottomContainer.Children)
            {
                danmakus.Add(item.Tag as DanmakuModel);
            }
        }

        if (danmakuLocation is null or DanmakuLocation.Scroll)
        {
            foreach (Grid item in _scrollContainer.Children)
            {
                danmakus.Add(item.Tag as DanmakuModel);
            }
        }

        return danmakus;
    }

    /// <summary>
    /// 隐藏弹幕.
    /// </summary>
    /// <param name="location">需要隐藏的位置.</param>
    public void HideDanmaku(DanmakuLocation location)
    {
        if (!_isApplyTemplate)
        {
            return;
        }

        switch (location)
        {
            case DanmakuLocation.Scroll:
                _scrollContainer.Visibility = Visibility.Collapsed;
                break;
            case DanmakuLocation.Top:
                _topContainer.Visibility = Visibility.Collapsed;
                break;
            case DanmakuLocation.Bottom:
                _bottomContainer.Visibility = Visibility.Collapsed;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 显示弹幕.
    /// </summary>
    /// <param name="location">需要显示的位置.</param>
    public void ShowDanmaku(DanmakuLocation location)
    {
        if (!_isApplyTemplate)
        {
            return;
        }

        switch (location)
        {
            case DanmakuLocation.Scroll:
                _scrollContainer.Visibility = Visibility.Visible;
                break;
            case DanmakuLocation.Top:
                _topContainer.Visibility = Visibility.Visible;
                break;
            case DanmakuLocation.Bottom:
                _bottomContainer.Visibility = Visibility.Visible;
                break;
            default:
                break;
        }
    }
}
