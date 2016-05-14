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

namespace WpfPanelDemo
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

        private void Grid_Clicked(object sender, RoutedEventArgs e)
        {
            var window = new Window() { Width = 300, Height = 300 , Title = "Grid"};
            window.Content = new Grid();
            window.Show();
        }

        private void StackPanel_Clicked(object sender, RoutedEventArgs e)
        {
            var window = new Window() { Width = 300, Height = 300, Title = "StackPanel" };
            window.Content = new StackPanel();
            window.Show();
        }

        private void DockPanel_Clicked(object sender, RoutedEventArgs e)
        {
            var window = new Window() { Width = 300, Height = 300, Title = "DockPanel" };
            window.Content = new DockPanel();
            window.Show();
        }

        private void WrapPanel_Clicked(object sender, RoutedEventArgs e)
        {
            var window = new Window() { Width = 300, Height = 300, Title = "WrapPanel" };
            window.Content = new WrapPanel();
            window.Show();
        }

        private void Canvas_Clicked(object sender, RoutedEventArgs e)
        {
            var window = new Window() { Width = 300, Height = 300, Title = "Canvas" };
            window.Content = new Canvas();
            window.Show();
        }
    }
}
