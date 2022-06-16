using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ManagementWindow.BaseClass;
using ManagementWindow.SQL;
using ManagementWindow.View;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace ManagementWindow.ViewModel
{
    public  class ItemManagementViewModel : ObservableObject
    {
        public AppData AppData { get; set; } = AppData.Instance;
        public RelayCommand<ListView> DeleteItems
        {
            get
            {
                return new RelayCommand<ListView>((res) =>
                {
                    ItemMes mes = res.SelectedItem as ItemMes;
                    if (MessageBox.Show(string.Format("是否删除项目号为 {0} 的项目信息", mes.ItemNo), "警告", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        int i = SqlAssociated.DeleteItemFormUI_ItemManagement(mes);
                        if (i != 0)
                        {
                            AppData.MainWindow.container.Content = new ItemManagementWindow();
                        }
                    }
                    else
                    {
                        return;
                    }
                });
            }
        }

        public RelayCommand AddItemsl
        {
            get
            {
                return new RelayCommand(() =>
                {
                    AddItems items = new AddItems();
                    items.ShowDialog();
                });
            }
        }

        public RelayCommand<ListView> BindingItems
        {
            get
            {
                return new RelayCommand<ListView>((res) =>
                {
                    ItemMes mes = res.SelectedItem as ItemMes;
                    BindingItemWindow binding = new BindingItemWindow(mes.ItemNo);
                    binding.ShowDialog();
                });
            }
        }
    }
}
