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
using System.Windows.Shapes;

namespace WpfGraphDrawing
{
    /// <summary>
    /// Interaction logic for EditPosition.xaml
    /// </summary>
    public partial class EditPosition : Window
    {
        public EditPosition()
        {
            InitializeComponent();
        }

        private void OK_Clicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
