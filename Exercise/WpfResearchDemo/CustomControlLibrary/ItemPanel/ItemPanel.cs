using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CustomControlLibrary
{
    [TemplatePart(Name = "PART_Path", Type = typeof(Path))]
    public class ItemPanel : ContentControl
    {
        static ItemPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemPanel), new FrameworkPropertyMetadata(typeof(ItemPanel)));
        }



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ItemPanel), new PropertyMetadata(null));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var partPath = this.GetTemplateChild("PART_Path") as Path;
            if (partPath != null)
            {
                var pathGeometry = new PathGeometry();
                var ellipsePathFigure = new PathFigure() { StartPoint = new Point(0, 20) };
                ellipsePathFigure.Segments = new PathSegmentCollection();
                ellipsePathFigure.Segments.Add(new ArcSegment()
                {
                    Size = new Size(5.0 / 6.0 * this.Width, 25),
                    Point = new Point(this.Width, 20),
                    IsLargeArc = false,
                    SweepDirection = SweepDirection.Counterclockwise
                });
                pathGeometry.Figures.Add(ellipsePathFigure);

                var linePathFigure = new PathFigure();
                linePathFigure.Segments = new PathSegmentCollection();
                linePathFigure.Segments.Add(new LineSegment() { Point = new Point(this.Width, 0) });
                linePathFigure.Segments.Add(new LineSegment() { Point = new Point(this.Width, 20) });
                linePathFigure.Segments.Add(new LineSegment() { Point = new Point(0, 20) });
                
                pathGeometry.Figures.Add(linePathFigure);

                partPath.Data = pathGeometry;
            }

        }
    }
}
