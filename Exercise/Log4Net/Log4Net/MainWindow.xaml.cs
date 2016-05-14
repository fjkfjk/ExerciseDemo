using System.Windows;
using log4net;
using log4net.Config;
using System.Reflection;
using System;

namespace Log4NetDemo
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

        private void btnFatal_Click(object sender, RoutedEventArgs e)
        {
            LogHelper.InsertLog(typeof(MainWindow), "This is a FATAL message.", EnumPublic.EnumLogRank.Fatal);
        }

        private void btnError_Click(object sender, RoutedEventArgs e)
        {
            LogHelper.InsertLog(typeof(MainWindow), "This is a ERROR message.", EnumPublic.EnumLogRank.Error);
        }

        private void btnWarn_Click(object sender, RoutedEventArgs e)
        {
            LogHelper.InsertLog(typeof(MainWindow), "This is a WARN message.", EnumPublic.EnumLogRank.Warn);
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            LogHelper.InsertLog(typeof(MainWindow), "This is a INFO message.", EnumPublic.EnumLogRank.Info);
        }

        private void btnDebug_Click(object sender, RoutedEventArgs e)
        {
            LogHelper.InsertLog(typeof(MainWindow), "This is a DEBUG message.", EnumPublic.EnumLogRank.Debug);
        }
    }
}
