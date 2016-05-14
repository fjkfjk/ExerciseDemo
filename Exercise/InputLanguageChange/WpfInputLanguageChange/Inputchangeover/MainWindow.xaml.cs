using System.Collections;
using System.Windows;
using System.Windows.Input;
using System.Globalization;
namespace Inputchangeover
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ArrayList languages = new ArrayList();
        int i = 0;
        public MainWindow()
        {
            InitializeComponent();
            foreach (var language in InputLanguageManager.Current.AvailableInputLanguages)                 //得到当前可用的输入语言。
            {
                languages.Add(language);
            }
        }
        

        private void button_Click(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = (CultureInfo)languages[i];                //设置当前输入语言。
            if(i != languages.Count-1)
            {
                i++;
            }
            else
            {
                i = 0;
            }
        }
    }
}
