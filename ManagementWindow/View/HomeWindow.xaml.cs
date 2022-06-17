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
    /// HomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HomeWindow : UserControl
    {
        public HomeWindow()
        {
            InitializeComponent();

            this.Loaded += (e, s) =>
            {

                OrCale or = new OrCale();
                string cmdAll = "SELECT * FROM MES.\"ItemManagement\"";
                DataSet data = or.Query(cmdAll);
                double top1 = -20;
                double left1 = 0;
                double top2 = -20;
                double left2 = 0;
                double top3 = -20;
                double left3 = 0;
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    if (row["Department"] != null)
                    {
                        switch (row["Department"])
                        {
                            case 1:
                                if (left1 >= 75)
                                    continue;
                                automation.Children.Add(NewShow(row, top1, left1));
                                top1 += 40;
                                left1 += 15;
                                break;
                            case 2:
                                if (left2 >= 75)
                                    continue;
                                smart.Children.Add(NewShow(row, top2, left2));
                                top2 += 40;
                                left2 += 15;
                                break;
                            case 3:
                                if (left3 >= 75)
                                    continue;
                                IT.Children.Add(NewShow(row, top3, left3));
                                top3 += 40;
                                left3 += 15;
                                break;
                        }
                    }
                }
            };
        }

        public Grid NewShow(DataRow row, double top, double left)
        {
            Grid grid = new Grid
            {
                Style = this.FindResource("GridStyle1") as Style,
                Margin = new Thickness(left, top, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200,
                Height = 250,
            };
            HomeControl home = new HomeControl();
            if (row["ItemName"] != null)
            {
                home.textblock.Text = row["ItemName"].ToString();
            }
            if (row["ItemNo"] != null)
            {
                home.ItemNo.Text = row["ItemNo"].ToString();
            }
            if (row["StartTime"] != null)
            {
                home.StartTime.Text = row["StartTime"].ToString();
            }
            if (row["EndTime"] != null)
            {
                home.EndTime.Text = row["EndTime"].ToString();
            }
            if (row["ItemState"] != null)
            {
                home.Status.Text = ConverStatus(row["ItemState"].ToString());
            }
            if (row["Participants"] != null)
            {
                home.Personnel.Text = row["Participants"].ToString();
            }
            grid.Children.Add(home);
            return grid;
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
