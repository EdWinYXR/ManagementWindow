using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementWindow.ViewModel
{
    public class StaffWindowViewModel : ObservableObject
    {
        public AppData AppData { get; set; } = AppData.Instance;
    }
}
