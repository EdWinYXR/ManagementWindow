using ManagementWindow.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ManagementWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

           // AppData.Instance.MainWindow = this;

            this.Loaded += (e, s) =>
            {
                AppData.Instance.MainWindow.container.Content = new HomeWindow();
            };
            this.Closing+=(e, s)=>
            {
                Thread.Sleep(100);//等待系统保存内容
                Process.GetCurrentProcess().Kill();
            };
        }

        private void MobileWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
