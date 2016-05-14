using Hibernate.Domain;
using Hibernate.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Hibernate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private PatientRepository patientRepository = null;
        private Patient patient = null;
        public MainWindow()
        {
            InitializeComponent();
            patientRepository = new PatientRepository();
            patient = new Patient();
            patientRepository.Setup();
        }
        
        private void UIToData()
        {
            if (txtId.Text != "")
                patient.Id = Convert.ToInt32(txtId.Text.Trim());
            else
                patient.Id = 0;
            if (txtName.Text != "")
                patient.Name = txtName.Text.ToString();
            if (txtAge.Text != "")
                patient.Age = Convert.ToInt32(txtAge.Text.Trim());
            else
                patient.Age = 0;
        }
        
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.UtcNow;
            UIToData();
            patientRepository.Add(patient);
            lblTime.Content = DateTime.UtcNow.Subtract(dt).ToString();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            UIToData();
            patientRepository.Remove(patient);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UIToData();
            patientRepository.Update(patient);
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            //UIToData();
            //if (patient.Id != 0)
            //{
            //    txtName.Text = patientRepository.GetById(patient.Id).Name.ToString();
            //    txtAge.Text = patientRepository.GetById(patient.Id).Age.ToString();
            //    return;
            //}
            //else if (patient.Name != null)
            //{
            //    txtId.Text = patientRepository.GetByName(patient.Name).Id.ToString();
            //    txtAge.Text = patientRepository.GetByName(patient.Name).Age.ToString();
            //    return;
            //}
            //else if (patient.Age != 0)
            //{
            if (txtAge.Text != "")
            {
                patient.Age = Convert.ToInt32(txtAge.Text.Trim());
                StringBuilder strBuilder = new StringBuilder();
                foreach (var p in patientRepository.GetByAge(patient.Age))
                {
                    strBuilder.AppendFormat("Id:{0} Name:{1} Age:{2} \t\n", p.Id, p.Name, p.Age);
                }
                txtDisplay.Text = strBuilder.ToString();
                return;
            }
            //}
        }

        private void btnBatchInsert_Click(object sender, RoutedEventArgs e)
        {
            int times = Convert.ToInt32(txtTimes.Text.Trim());
            //Patient p = new Patient();
            DateTime dt = DateTime.UtcNow;
            //for(int i = 0; i <= times; i++)
            //{
            //    p.Id = i;
            //    p.Name = i.ToString();
            //    p.Age = i;
            //    patientRepository.Add(p);
            //}
            patientRepository.BatchAdd(times);
            lblTime.Content = DateTime.UtcNow.Subtract(dt).ToString();
        }
    }
}
