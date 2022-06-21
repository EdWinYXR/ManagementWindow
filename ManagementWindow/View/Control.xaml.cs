using ManagementWindow.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Control.xaml 的交互逻辑
    /// </summary>
    public partial class Control : UserControl
    {
        public Control()
        {
            InitializeComponent();

            AppData.Instance.ControlViewModel = this.DataContext as ControlViewModel;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
           AppData.Instance.MainWindow.stack.Children.Clear();
            //Thread.Sleep(100);
            string[] str = this.Personnel.Text.Split(',');
            foreach(string s in str)
            {
                AppData.Instance.MainWindow.stack.Children.Add(new ShowPersonnelWindow(s));
            }
            Point point = Mouse.GetPosition(AppData.Instance.MainWindow.container);
            AppData.Instance.MainWindow.mouseShow.Margin = new Thickness(point.X-50, point.Y-100, 0, 0);
            AppData.Instance.MainWindow.mouseShow.Visibility = Visibility.Visible;
        }
    }
}
