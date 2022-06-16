using ManagementWindow.BaseClass;
using ManagementWindow.SQL;
using SQL;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace ManagementWindow.View
{
    /// <summary>
    /// StaffWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StaffWindow : UserControl
    {
        public StaffWindow()
        {
            InitializeComponent();

            this.Loaded += (e, s) =>
            {
                this.Setff.DataContext = SqlAssociated.CmdAllPersonndelGetUI();
                //ObservableCollection<object> ObservableObj = new ObservableCollection<object>();
                //ObservableObj.Add(new { ID = 1, Name = "Alex1", ItemNum = "男", Email = 10 , Phone =1111,});
                //this.listSetff.DataContext = ObservableObj;
            };
        }
    }
}
