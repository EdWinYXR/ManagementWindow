using ManagementWindow.BaseClass;
using SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// PersonnelManagement.xaml 的交互逻辑
    /// </summary>
    public partial class PersonnelManagement : UserControl
    {
        public PersonnelManagement()
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
                foreach (DataRow row in data.Tables[0].Rows)
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
                    if (row["Department"] != null)
                    {
                        staff.Department = row["Department"].ToString();
                    }
                    L_listStaff.Add(staff);
                }
                lvUsers.ItemsSource = L_listStaff;

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Department");
                view.GroupDescriptions.Add(groupDescription);
                //排序
                view.SortDescriptions.Add(new SortDescription("ItemNum", ListSortDirection.Ascending));
                //搜索
                view.Filter = UserFilter;
            };
        }
        /// <summary>
        /// 过滤器
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as Staff).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvUsers.ItemsSource).Refresh();
        }
    }
}
