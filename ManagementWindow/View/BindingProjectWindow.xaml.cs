using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using ManagementWindow.BaseClass;
using ManagementWindow.SQL;
namespace ManagementWindow.View
{
    /// <summary>
    /// BindingProjectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BindingProjectWindow : Window
    {
        public BindingProjectWindow()
        {
            InitializeComponent();

            AppData.Instance.BindingProjectWindow = this;

            this.Loaded += (e, s) =>
            {
                List<ItemMes> listmes = SqlAssociated.CmdItemMESGetUI();
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
