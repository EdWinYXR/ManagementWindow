using ManagementWindow.View;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
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
                            AppData.Instance.MainWindow.container.Content = new WindowShow1();
                            break;
                        case "Window2":
                            break;
                        case "Window3":

                            break;
                        case "Window4":
                            break;
                    }
                });
            }
        }
    }
}
