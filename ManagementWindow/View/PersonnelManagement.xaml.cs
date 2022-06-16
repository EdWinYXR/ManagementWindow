﻿using ManagementWindow.BaseClass;
using ManagementWindow.SQL;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

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
   
                lvUsers.ItemsSource = SqlAssociated.CmdAllPersonndelGetUI();

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
    }
}