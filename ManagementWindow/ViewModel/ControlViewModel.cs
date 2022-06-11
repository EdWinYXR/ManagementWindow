using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ManagementWindow.ViewModel
{
    public class ControlViewModel : ObservableObject
    {
        public AppData AppData { get; set; } = AppData.Instance;
    }
}
