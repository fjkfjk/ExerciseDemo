using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MultiLang
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += App_Startup;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            //第一种方式就是用配置文件 配置
            string appLang = "Resources/StringDictionary.xaml";//ConfigurationManager.AppSettings.Get("Languages");
            App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() 
            { Source = new Uri(appLang, UriKind.RelativeOrAbsolute) });
        }

    }
}
