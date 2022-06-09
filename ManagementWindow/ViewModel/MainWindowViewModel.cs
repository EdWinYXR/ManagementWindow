using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                  
                });
            }
        }
        public RelayCommand<Window> MaxWindow
        {
            get
            {
                return new RelayCommand<Window>((res) =>
                {

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
                        case "Window1":
                            AppData.Instance.MainWindow.container.Content = new Window1Control();
                            break;
                        case "Window2":
                            break;
                        case "Window3":
                            AppData.Instance.MainWindow.container.Content = new Window3Control();
                            break;
                        case "Window4":
                            AppData.Instance.MainWindow.container.Content = new Window4Control();
                            break;
                    }
                });
            }
        }
    }
}
