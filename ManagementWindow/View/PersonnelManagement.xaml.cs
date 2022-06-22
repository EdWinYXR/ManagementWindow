using ManagementWindow.BaseClass;
using ManagementWindow.SQL;
using ManagementWindow.ViewModel;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ManagementWindow.View
{
    /// <summary>
    /// PersonnelManagement.xaml 的交互逻辑
    /// </summary>
    public partial class PersonnelManagement : UserControl
    {
        public PersonnelManagement()
        {
            InitializeComponent();

            AppData.Instance.PersonnelManagement = this;

            this.Loaded += (e, s) =>
            {
   
                lvUsers.ItemsSource = SqlAssociated.CmdAllPersonndelGetUI("","");

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Department");
                view.GroupDescriptions.Add(groupDescription);
                //排序
                view.SortDescriptions.Add(new SortDescription("ItemNum", ListSortDirection.Ascending));
                //搜索
                view.Filter = UserFilter;

            };
        }
        /// <summary>
        /// 过滤器
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as Staff).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvUsers.ItemsSource).Refresh();
        }

        private void starte_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = this.DataContext as PersonnelManagementViewModel;
            lvUsers.ItemsSource = vm.SelectedDateChanged();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Department");
            view.GroupDescriptions.Add(groupDescription);
            //排序
            view.SortDescriptions.Add(new SortDescription("ItemNum", ListSortDirection.Ascending));
            //搜索
            view.Filter = UserFilter;
        }

        private void lvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (starte.SelectedDate != null && end.SelectedDate != null)
            {
                var vm = this.DataContext as PersonnelManagementViewModel;
                Staff staff = this.lvUsers.SelectedItem as Staff;
                if (staff != null)
                {
                    vm.LoadingChart(staff.ID);
                    vm.GetXAxisX();
                    this.grid.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.grid.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
