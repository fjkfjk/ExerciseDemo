using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfGraphDrawing
{
    public class DragDropBehavior
    {

        public static Canvas GetPlacementTarget(UIElement obj)
        {
            return (Canvas)obj.GetValue(PlacementTarget);
        }

        public static void SetPlacementTarget(UIElement obj, Canvas value)
        {
            obj.SetValue(PlacementTarget, value);
        }

        // Using a DependencyProperty as the backing store for PlacementTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlacementTarget =
            DependencyProperty.RegisterAttached("PlacementTarget", typeof(Canvas), typeof(DragDropBehavior), new PropertyMetadata(null, OnPlacementTargetChanged));

       
        private static void OnPlacementTargetChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var source = sender as UIElement;
            var placementTarget = args.NewValue as Canvas;

            bool captured = false;
            double x_shape = 0, x_canvas = 0, y_shape = 0, y_canvas = 0;
            

            if (source != null && placementTarget != null)
            {
                source.MouseLeftButtonDown += (s, e) =>
                {
                    Debug.WriteLine("MouseLeftButtonDown event captured.");
                    Mouse.Capture(source);
                    captured = true;
                    x_shape = Canvas.GetLeft(source);
                    x_canvas = e.GetPosition(placementTarget).X;
                    y_shape = Canvas.GetTop(source);
                    y_canvas = e.GetPosition(placementTarget).Y;
                };

                source.MouseMove += (s, e) =>
                 {
                     if (captured)
                     {
                         double x = e.GetPosition(placementTarget).X;
                         double y = e.GetPosition(placementTarget).Y;

                         Debug.WriteLine(string.Format("MouseMove event captured. Xoffset: {0}, Yoffset: {1}.", x- x_canvas, y - y_canvas));

                         x_shape += x - x_canvas;
                         Canvas.SetLeft(source, x_shape);
                         x_canvas = x;
                         y_shape += y - y_canvas;
                         Canvas.SetTop(source, y_shape);
                         y_canvas = y;
                     }
                 };

                source.MouseLeftButtonUp += (s, e) =>
                 {
                     Debug.WriteLine("MouseLeftButtonUp event captured.");
                     Mouse.Capture(null);
                     captured = false;
                 };
            }
        }
    }
}
