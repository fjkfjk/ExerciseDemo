using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 监测网线插拔
{
    public partial class Form1 : Form
    {
        [DllImport("Wininet.dll")]
        private static extern bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        private const int INTERNET_CONNECTION_OFFLINE = 32;
        public Form1()
        {
            InitializeComponent();
        }

        static Timer myTimer = new Timer(); //新建一个Timer对象

        private void button1_Click(object sender, EventArgs e)
        {
            //为Timer对象的Tick事件添加事件处理程序
            myTimer.Tick += checkConnected;

            //设置Timer的间隔interval，启动Timer，当Timer的Enable属性为true,Interval属性大于0时，将引发Timer事件。
            myTimer.Interval = 1000;
            myTimer.Enabled = true;
            myTimer.Start();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //窗口关闭时，释放Timer实例的资源
            myTimer.Dispose();

        }
        //Tick事件的处理程序，用来监测网线插拔的函数
        private void checkConnected(object sender,EventArgs e)
        {
            int i = 0;
            bool isConnected = InternetGetConnectedState(out i, 0);
            if((i&INTERNET_CONNECTION_OFFLINE) == INTERNET_CONNECTION_OFFLINE)
            {
                MessageBox.Show("网络已断线！");
            }
            else
            {
                if(!isConnected)
                {
                    myTimer.Enabled = false;
                    MessageBox.Show("没有连网！");                
                }
                     
            }            
        }

    }
}
