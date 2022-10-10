﻿using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace OpenWeatherMap.Wpf.LiveMap;

public class ScrollMouseWheelBehavior : Behavior<UIElement>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.PreviewMouseWheel += AssociatedObject_PreviewMouseWheel;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.PreviewMouseWheel -= AssociatedObject_PreviewMouseWheel;
        base.OnDetaching();
    }

    void AssociatedObject_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        e.Handled = true;
        var e2 = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
        e2.RoutedEvent = UIElement.MouseWheelEvent;
        AssociatedObject.RaiseEvent(e2);
    }
}