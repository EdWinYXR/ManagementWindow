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
    /// ItemWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ItemWindow : UserControl
    {
        public ItemWindow()
        {
            InitializeComponent();

            this.Loaded += (e, s) =>
            {
                this.ViemBorder.Children.Clear();
                OrCale or = new OrCale();
                string cmdAll = "SELECT * FROM MES.\"ItemManagement\"";
                DataSet data = or.Query(cmdAll);
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    View.Control control = new View.Control();
                    if (row["ItemName"] != null)
                    {
                        control.textblock.Text = row["ItemName"].ToString();
                    }
                    if (row["ItemNo"] != null)
                    {
                        control.ItemNo.Text = row["ItemNo"].ToString();
                    }
                    if (row["StartTime"] != null)
                    {
                        control.StartTime.Text = row["StartTime"].ToString();
                    }
                    if (row["EndTime"] != null)
                    {
                        control.EndTime.Text = row["EndTime"].ToString();
                    }
                    if (row["ItemState"] != null)
                    {
                        control.Status.Text = ConverStatus(row["ItemState"].ToString());
                    }
                    if (row["Participants"] != null)
                    {
                        control.Personnel.Text = row["Participants"].ToString();
                    }
                    this.ViemBorder.Children.Add(control);
                }
            };
        }

        public string ConverStatus(string s)
        {
            string status = "";
            switch (s)
            {
                case "1":
                    status = "等待";
                    break;
                case "2":
                    status = "施工";
                    break;
                case "3":
                    status = "调试";
                    break;
                case "4":
                    status = "验收";
                    break;
                case "5":
                    status = "完成";
                    break;
            }
            return status;
        }
    }
}
