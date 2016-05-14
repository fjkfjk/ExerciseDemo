using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGraphDrawing.ViewModels
{
    public class EditPositionVm : NotifyPropertyChangedBase
    {
        public double Left { get; set; }
        public double Top { get; set; }
    }
}
