using ManagementWindow.BaseClass;
using ManagementWindow.SQL;
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
    /// ItemManagementWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ItemManagementWindow : UserControl
    {
        public ItemManagementWindow()
        {
            InitializeComponent();

            this.Loaded += (e, s) =>
            {
                List<ItemMes> listmes = SqlAssociated.CmdAllItemMESGetUI();
                lvUsers.ItemsSource = listmes;

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
                //PropertyGroupDescription groupDescription = new PropertyGroupDescription("Department");
                //view.GroupDescriptions.Add(groupDescription);
                ////排序
                //view.SortDescriptions.Add(new SortDescription("ItemNum", ListSortDirection.Ascending));
                //搜索
                view.Filter = UserFilter;
            };
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as ItemMes).ItemName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvUsers.ItemsSource).Refresh();
        }
    }
}
