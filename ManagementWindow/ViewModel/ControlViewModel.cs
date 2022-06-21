using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementWindow.BaseClass;
using ManagementWindow.SQL;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ManagementWindow.ViewModel
{
    public class ControlViewModel : ObservableObject
    {
        public ControlViewModel()
        {
            showPersonnelManage = SqlAssociated.CmdItemsStaffGetControl();
        }
        public AppData AppData { get; set; } = AppData.Instance;

        public  List<ShowPersonnelManage> showPersonnelManage { get; set; } = null;
    }
}
