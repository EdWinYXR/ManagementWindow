﻿using ManagementWindow.View;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SQL;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ManagementWindow.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        public AppData AppData { get; set; } = AppData.Instance;

        public RelayCommand<Window> MinWindow
        {
            get
            {
                return new RelayCommand<Window>((res) =>
                {
                    res.WindowState = WindowState.Minimized;
                });
            }
        }
        public RelayCommand<Window> MaxWindow
        {
            get
            {
                return new RelayCommand<Window>((res) =>
                {
                    if (res.WindowState == WindowState.Maximized)
                        res.WindowState = WindowState.Normal;
                    else
                        res.WindowState = WindowState.Maximized;
                });
            }
        }
        public RelayCommand<Window> CloseWindow
        {
            get
            {
                return new RelayCommand<Window>((res) =>
                {
                    res.Close();
                });
            }
        }
        /// <summary>
        /// 切换页面
        /// </summary>
        public RelayCommand<ContentControl> ShowWindow
        {
            get
            {
                return new RelayCommand<ContentControl>((res) =>
                {
                    if (!(res is System.Windows.Controls.RadioButton)) return;
                    switch (res.Content)
                    {
                        case "首页":
                           AppData.MainWindow.container.Content=new  ItemWindow(); 
                            break;
                        case "员工信息":
                           AppData.MainWindow.container.Content=new  StaffWindow(); 
                            break;
                        case "人员管理":

                            break;
                        case "项目管理":
                            break;
                    }
                });
            }
        }
    }
}
