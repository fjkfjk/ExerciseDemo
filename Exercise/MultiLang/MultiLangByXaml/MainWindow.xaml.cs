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

namespace MultiLang
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnzhCN_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("Resources/StringDictionary.zh-CN.xaml", UriKind.RelativeOrAbsolute) });

            //代码中使用多语言
            tbUsername.Text = Application.Current.Resources["UserName"].ToString();//Application.Current.FindResource("UserName").ToString(); ;
        }

        private void btnenUS_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("Resources/StringDictionary.xaml", UriKind.RelativeOrAbsolute) });

            //代码中使用多语言
            tbUsername.Text = Application.Current.FindResource("UserName").ToString(); ;
        }
    }
}
