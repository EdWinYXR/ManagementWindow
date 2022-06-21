using ManagementWindow.ViewModel;
using System;
using System.Collections.Generic;
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
    /// ShowPersonnelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShowPersonnelWindow : UserControl
    {
        public ShowPersonnelWindow(string name)
        {
            InitializeComponent();

            this.Loaded += (e, s) =>
            {
                var vm = this.DataContext as ShowPersonnelViewModel;
                vm.Assignment(name);
            };
        }
    }
}
