using ManagementWindow.BaseClass;
using ManagementWindow.SQL;
using ManagementWindow.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace ManagementWindow.View
{
    /// <summary>
    /// BindingItemWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BindingItemWindow : Window
    {
        public string ItemNo { get; set; }
        public BindingItemWindow(string ItemNowin)
        {
            InitializeComponent();
            ItemNo = ItemNowin;
            lvUsers.ItemsSource = SqlAssociated.CmdAllPersonndelGetUI("","");

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Department");
            view.GroupDescriptions.Add(groupDescription);
            //排序
            view.SortDescriptions.Add(new SortDescription("ItemNum", ListSortDirection.Ascending));
            //搜索
            view.Filter = UserFilter;
        }


        /// <summary>
        /// 过滤器
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text)&& (comtextFilter.SelectedItem as ComboBoxItem).Content.ToString() == "级别")
                return true;
            else if (txtFilter.Text!=null && (comtextFilter.SelectedItem as ComboBoxItem).Content.ToString() == "级别")
            {
                return ((item as Staff).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            else
                return ((item as Staff).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0&& 
                    (item as Staff).grade.IndexOf((comtextFilter.SelectedItem as ComboBoxItem).Content.ToString(), StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvUsers.ItemsSource).Refresh();
        }

        private void comtextFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvUsers.ItemsSource).Refresh();
        }

        private void comtextFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0)
            {
                CollectionViewSource.GetDefaultView(lvUsers.ItemsSource).Refresh();
            }
        }
        private void OnListViewItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = this.DataContext as BindingItemViewModel;
            vm.ItemNo = ItemNo;
            vm.DoubleClickBinding(lvUsers.SelectedItem as Staff,ItemNo);
            AppData.Instance.BindingItemViewModel = vm;
            this.DialogResult = true;
            this.Close();
        }
        private void starte_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = this.DataContext as BindingItemViewModel;
            lvUsers.ItemsSource = vm.SelectedDateChanged();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Department");
            view.GroupDescriptions.Add(groupDescription);
            //排序
            view.SortDescriptions.Add(new SortDescription("ItemNum", ListSortDirection.Ascending));
            //搜索
            view.Filter = UserFilter;
        }
    }
}
