using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputLanguagechangeover
{
    public partial class Form1 : Form
    {
        ArrayList languages = new ArrayList();                                                              //存放所有输入语言
        int i = 1;
        public Form1()
        {
            InitializeComponent();        
        }              
        private void button1_Click(object sender, EventArgs e)
        {
            languages.Clear();
            foreach (var language in InputLanguage.InstalledInputLanguages)                                //获取所有已安装输入语言
            {
                languages.Add(language);
            }
            InputLanguage.CurrentInputLanguage = (InputLanguage)languages[i];                             //设置当前线程的输入语言
            textBox1.Text = InputLanguage.CurrentInputLanguage.LayoutName;
            if (i == languages.Count-1)
            {
                i = 0;
            }
            else
            {
                i++;
            }
        }
    }
}
