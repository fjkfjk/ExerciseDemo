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

namespace SQLiteDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SQLiteProcess.CreateTable();
        }

        public void Display()
        {
            List<User> users = SQLiteProcess.Query();
            StringBuilder txtBuilding = new StringBuilder();
            foreach (User user in users)
            {
                txtBuilding.AppendFormat("ID: {0} name: {1} email: {2}\r\n", user.ID, user.name, user.email);
            }
            txtDisplay.Text = txtBuilding.ToString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string[] inputs = txtInput.Text.Trim().Split(',');
            SQLiteProcess.Insert(inputs[0], inputs[1]);
            Display();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int input = Convert.ToInt32(txtInput.Text.Trim());
            SQLiteProcess.Delete(input);
            Display();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string[] inputs = txtInput.Text.Trim().Split(',');
            SQLiteProcess.Update(inputs[1], inputs[2], Convert.ToInt32(inputs[0]));
            Display();
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            Display();
        }
    }
}
