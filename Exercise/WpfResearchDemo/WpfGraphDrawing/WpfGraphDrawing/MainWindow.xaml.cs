using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfGraphDrawing.ViewModels;

namespace WpfGraphDrawing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawLine_Clicked(object sender, RoutedEventArgs e)
        {
            var window = new EditPosition() { Owner = this, WindowStartupLocation = WindowStartupLocation.CenterOwner };
            var vm = new EditPositionVm();
            window.DataContext = vm;

            window.ShowDialog();

            if (window.DialogResult == true)
            {
                var line = new Line() { X1 = 0, Y1 = 0, X2 = 150, Y2 = 320 ,Stroke = new SolidColorBrush(Colors.Red), StrokeThickness = 2};
                Canvas.SetLeft(line, vm.Left);
                Canvas.SetTop(line, vm.Top);
                this.DrawCanvas.Children.Add(line);

                //Add drag drop behavior
                line.SetValue(DragDropBehavior.PlacementTarget, this.DrawCanvas);
            }

            
        }

        private void DrawCircle_Clicked(object sender, RoutedEventArgs e)
        {
            var window = new EditPosition() { Owner = this, WindowStartupLocation = WindowStartupLocation.CenterOwner };
            var vm = new EditPositionVm();
            window.DataContext = vm;

            window.ShowDialog();
            

            if (window.DialogResult == true)
            {
                var ellipse = new Ellipse() { Width = 100, Height = 100, Stroke = new SolidColorBrush(Colors.Gold), StrokeThickness = 2 , Fill = new SolidColorBrush(Colors.Green)};
                Canvas.SetLeft(ellipse, vm.Left);
                Canvas.SetTop(ellipse, vm.Top);
                this.DrawCanvas.Children.Add(ellipse);

                //Add drag drop behavior
                ellipse.SetValue(DragDropBehavior.PlacementTarget, this.DrawCanvas);

            }
        }

        /// <summary>
        /// Draw multiple geometry at one time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawPath_Clicked(object sender, RoutedEventArgs e)
        {
            // Create a path
            Path path = new Path();
            path.Stroke = new SolidColorBrush(Colors.BlueViolet);
            path.StrokeThickness = 3;

            // Create a line geometry
            LineGeometry blackLineGeometry = new LineGeometry();
            blackLineGeometry.StartPoint = new Point(20, 200);
            blackLineGeometry.EndPoint = new Point(300, 200);

            // Create an ellipse geometry
            EllipseGeometry blackEllipseGeometry = new EllipseGeometry();
            blackEllipseGeometry.Center = new Point(80, 150);
            blackEllipseGeometry.RadiusX = 50;
            blackEllipseGeometry.RadiusY = 50;

            // Create a rectangle geometry
            RectangleGeometry blackRectGeometry = new RectangleGeometry();
            Rect rct = new Rect();
            rct.X = 80;
            rct.Y = 167;
            rct.Width = 150;
            rct.Height = 30;
            blackRectGeometry.Rect = rct;

            // Add all the geometries to a GeometryGroup.
            GeometryGroup blueGeometryGroup = new GeometryGroup();
            blueGeometryGroup.Children.Add(blackLineGeometry);
            blueGeometryGroup.Children.Add(blackEllipseGeometry);
            blueGeometryGroup.Children.Add(blackRectGeometry);

            // Set Path.Data
            path.Data = blueGeometryGroup;

            //Add drag drop behavior.
            path.SetValue(DragDropBehavior.PlacementTarget, this.DrawCanvas);

            Canvas.SetLeft(path, 0);
            Canvas.SetTop(path, 0);
            this.DrawCanvas.Children.Add(path);
        }

        /// <summary>
        /// Draw segments and connect them together.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void DrawPathSegments_Clicked(object sender, RoutedEventArgs e)
        {
            // Create a path
            Path path = new Path();
            path.Stroke = new SolidColorBrush(Colors.Black);
            path.StrokeThickness = 2;

            var pathGeometry = new PathGeometry();

            //create segments
            PathSegmentCollection segmentCollection = new PathSegmentCollection();
            segmentCollection.Add(new LineSegment() { Point = new Point(300, 400) });
            segmentCollection.Add(new LineSegment() { Point = new Point(400, 300) });
            segmentCollection.Add(new LineSegment() { Point = new Point(500, 300) });

            //create path feature
            var pathFigure = new PathFigure() { Segments = segmentCollection };
            pathFigure.StartPoint = new Point(150, 10);
            pathGeometry.Figures = new PathFigureCollection() { pathFigure };

            path.Data = pathGeometry;

            //Add drag drop behavior.
            path.SetValue(DragDropBehavior.PlacementTarget, this.DrawCanvas);

            Canvas.SetLeft(path, 0);
            Canvas.SetTop(path, 0);
            this.DrawCanvas.Children.Add(path);
        }

        private void DrawText_Clicked(object sender, RoutedEventArgs e)
        {
            var textBox = new TextBox();
            textBox.Text = "Add some comments here...";
            textBox.AcceptsReturn = true;
            textBox.Background = new SolidColorBrush(Colors.Transparent);
            textBox.BorderThickness = new Thickness(0);
            Canvas.SetLeft(textBox, 400);
            Canvas.SetTop(textBox, 200);
            this.DrawCanvas.Children.Add(textBox);
        }
    }
}
