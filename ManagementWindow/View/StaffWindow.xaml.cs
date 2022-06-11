using ManagementWindow.BaseClass;
using SQL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                List<Staff> L_listStaff = new List<Staff>();
                OrCale or = new OrCale();
                string cmdall = "SELECT * FROM \"Staff\" ORDER BY \"ItemNum\"";
                DataSet data = or.Query(cmdall);
                if (data.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                foreach(DataRow row in data.Tables[0].Rows)
                {
                    Staff staff = new Staff();
                    if (row["ID"] != null)
                    {
                        staff.ID = row["ID"].ToString();
                    }
                    if (row["Name"] != null)
                    {
                        staff.Name = row["Name"].ToString();
                    }
                    if (row["ItemNum"] != null)
                    {
                        staff.ItemNum = row["ItemNum"].ToString();
                    }
                    if (row["Email"] != null)
                    {
                        staff.Email = row["Email"].ToString();
                    }
                    if (row["Phone"] != null)
                    {
                        staff.Phone = row["Phone"].ToString();
                    }
                    L_listStaff.Add(staff);
                }
                this.listSetff.DataContext = L_listStaff;
                //ObservableCollection<object> ObservableObj = new ObservableCollection<object>();
                //ObservableObj.Add(new { ID = 1, Name = "Alex1", ItemNum = "男", Email = 10 , Phone =1111,});
                //this.listSetff.DataContext = ObservableObj;
            };
        }
    }
}
