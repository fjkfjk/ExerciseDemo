using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializeAndDeserialize
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

        private string fileName = "patient.xml";//文件名称与路径
        public Patient UIToData()
        {
            Patient patient = new Patient();
            patient.report = new Report();
            patient.name = txtName.Text;
            patient.id = Convert.ToInt32(txtId.Text);
            patient.age = Convert.ToInt32(txtAge.Text);
            patient.report.reportId = Convert.ToInt32(txtReportId.Text);
            patient.report.report1 = txtReport1.Text;
            patient.report.report2 = txtReport2.Text;
            return patient;
        }

        public void DataToUI(Patient p)
        {
            txtName.Text = p.name;
            txtId.Text = p.id.ToString();
            txtAge.Text = p.age.ToString();
            txtReportId.Text = p.report.reportId.ToString();
            txtReport1.Text = p.report.report1;
            txtReport2.Text = p.report.report2;
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            Patient p = new Patient();

            using (Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {

                if (rdBtnSoap.IsChecked == true)
                {
                    SoapFormatter soapFormat = new SoapFormatter();
                    p = (Patient)soapFormat.Deserialize(fStream);
                }
                else
                {
                    BinaryFormatter binFormat = new BinaryFormatter();
                    p = (Patient)binFormat.Deserialize(fStream);
                }
            }
            DataToUI(p);
        }

        private void btnWrite_Click(object sender, RoutedEventArgs e)
        {
            Patient p = UIToData();

            using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite))
            {
                if (rdBtnSoap.IsChecked == true)
                {
                    SoapFormatter soapFormat = new SoapFormatter();
                    soapFormat.Serialize(fStream, p);
                }
                else
                {
                    BinaryFormatter binFormat = new BinaryFormatter();
                    binFormat.Serialize(fStream, p);
                }
            }
        }
    }
}
