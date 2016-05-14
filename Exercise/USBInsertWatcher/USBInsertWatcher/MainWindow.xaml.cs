using System;
using System.Windows;
using System.Management;

namespace USBInsertWatcher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            USBInsertWacher();
            USBRemoveWacher();
        }

        /// <summary>
        /// USB 插入和拔出监测函数。
        /// 使用ManagementEventWacher来预定特定系统事件，通过WqlEventQuery设置查询对象和条件以及其他属性（比如查询的轮询间隔），
        /// 通过ManagementScope设置查询路径范围。
        /// </summary>
        public void USBRemoveWacher()
        {
            ManagementEventWatcher wacher = null;
            WqlEventQuery query = null;
            ManagementScope scope = null;
            try
            {
                scope = new ManagementScope("root\\CIMV2");                                //设置WMI路径
                query = new WqlEventQuery();                                               //设置查询的事件类名，条件，查询间隔，也可一次在构造函数中初始化
                query.EventClassName = "__InstanceDeletionEvent";
                query.Condition = @"TargetInstance ISA 'Win32_USBControllerdevice'";
                query.WithinInterval = new TimeSpan(1000);
                wacher = new ManagementEventWatcher(scope, query);
                wacher.EventArrived += new EventArrivedEventHandler(onUSBRemoved);
                wacher.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Closed += (s, e) =>
              {
                  wacher.Stop();
                  wacher.Dispose();
              };
        }
        public void USBInsertWacher()
        {
            ManagementEventWatcher wacher = null;
            WqlEventQuery query = null;
            ManagementScope scope = null;
            try
            {
                scope = new ManagementScope("root\\CIMV2");                                //设置WMI路径
                query = new WqlEventQuery("__InstanceCreationEvent",@"TargetInstance ISA 'Win32_USBControllerdevice'");                          
                query.WithinInterval = new TimeSpan(1000);                                //设置查询的事件类名，条件，查询间隔.
                wacher = new ManagementEventWatcher(scope, query);
                wacher.EventArrived += new EventArrivedEventHandler(onUSBInsert);
                wacher.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Closed += (s, e) =>
            {
                wacher.Stop();
                wacher.Dispose();
            };
        }
        private void onUSBRemoved(object sender, EventArrivedEventArgs e)
        {
            MessageBox.Show("USB has been removed!");

        }
        private void onUSBInsert(object sender, EventArrivedEventArgs e)
        {
            MessageBox.Show("USB has been inserted!");
        }

    }
}
