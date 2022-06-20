using ManagementWindow.ViewModel;
using System.Windows;

namespace ManagementWindow.View
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
           _= new InIt();

            InitializeComponent();

            AppData.Instance.LoginModel = this;

        }
    }
}
