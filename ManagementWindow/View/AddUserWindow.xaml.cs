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
    /// AddUserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddUserWindow : UserControl
    {
        public AddUserWindow()
        {
            InitializeComponent();

            AppData.Instance.AddUserWindow = this;
        }

        private void CheckUsername_Changed(object sender, TextChangedEventArgs e)
        {
            var vm = DataContext as AddUserViewModel;
            var a = vm.CurrentUser.Find(Us => Us.username == textBoxUserName.Text);
            if (a != null)
            {
                this.TextUser.Visibility = Visibility.Visible;
                this.textBoxUserName.BorderBrush = Brushes.Red;
            }
            else
            {
                this.TextUser.Visibility = Visibility.Hidden;
                this.textBoxUserName.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFABADB3"));
                //#FFABADB3
            }
        }
        private void CheckPassword_Changed(object sender, TextChangedEventArgs e)
        {
            if (this.password.Text != this.textpassword.Text)
            {
                this.TextPassword.Visibility = Visibility.Visible;
                this.password.BorderBrush = Brushes.Red;
            }
            else
            {
                this.TextPassword.Visibility = Visibility.Hidden;
                this.password.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFABADB3"));
                //#FFABADB3
            }
        }
    }
}
