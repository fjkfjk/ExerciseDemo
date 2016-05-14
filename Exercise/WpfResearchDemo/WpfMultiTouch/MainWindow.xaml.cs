using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace WpfMultiTouch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> _filesList;
        private int _currentFile;
        private StylusPointCollection _downPoints;

        public MainWindow()
        {
            InitializeComponent();
            _filesList = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\Images", "*.jpg").ToList();
            _currentFile = 0;
            UpdateImage();
        }

        private void UpdateImage()
        {
            MainImage.Source = new BitmapImage(new Uri(_filesList[_currentFile]));
        }

        private void NextFile()
        {
            _currentFile = _currentFile + 1 == _filesList.Count ? 0 : _currentFile + 1;
            UpdateImage();
        }
        private void PreviousFile()
        {
            _currentFile = _currentFile == 0 ? _filesList.Count - 1 : _currentFile - 1;
            UpdateImage();
        }

        private void PrevClick(object sender, RoutedEventArgs e)
        {
            PreviousFile();
        }
        private void NextClick(object sender, RoutedEventArgs e)
        {
            NextFile();
        }


        private void GridGesture(object sender, StylusSystemGestureEventArgs e)
        {
            if (e.SystemGesture != SystemGesture.Drag)
                return;

            Debug.WriteLine("GridGesture event captured.");

            var newPoints = e.GetStylusPoints(MainImage);
            bool isReverse = false;
            if (newPoints.Count > 0 && _downPoints.Count > 0)
            {
                var distX = newPoints[0].X - _downPoints[0].X;
                var distY = newPoints[0].Y - _downPoints[0].Y;

                Debug.WriteLine(string.Format("Xoffset: {0}, Yoffset: {1}", distX, distY));

                if (Math.Abs(distX) > Math.Abs(distY))
                {
                    isReverse = distX < 0; // Horizontal
                }
                else
                {
                    return; // Vertical 

                }
            }
            if (isReverse)
            {
                PreviousFile();
            }
            else
            {
                NextFile();
            }
        }



        private void GridStylusDown(object sender, StylusDownEventArgs e)
        {
            _downPoints = e.GetStylusPoints(MainImage);
        }
    }
}
