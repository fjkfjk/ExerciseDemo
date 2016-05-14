using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMvvmUtilities;

namespace WpfCommand.ViewModels
{
    public class MainVm : NotifyPropertyChangedBase
    {
        public Command PatientManagementCommand { get; private set; }

        public MainVm()
        {
            this.PatientManagementCommand = new Command(ShowPatientManagement);
        }

        private void ShowPatientManagement()
        {
            MessageBox.Show("进入新增病人界面!");
        }
    }
}
