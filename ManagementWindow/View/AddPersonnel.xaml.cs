using System.Windows;
using ManagementWindow.ViewModel;

namespace ManagementWindow.View
{
    /// <summary>
    /// AddPersonnel.xaml 的交互逻辑
    /// </summary>
    public partial class AddPersonnel : Window
    {
        public AddPersonnel()
        {
            InitializeComponent();

            AppData.Instance.AddPersonnel = this;
        }
    }
}
