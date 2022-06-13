using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementWindow.ViewModel
{
    public class BindingProjectWindowViewModel : ObservableObject
    {
       public  AppData AppData { get; set; } = AppData.Instance;
    }
}
