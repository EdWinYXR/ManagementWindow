using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementWindow.ViewModel
{
    public class LoginWindowViewModel : ObservableObject
    {
        public AppData AppData { get; set; } = AppData.Instance;

        public RelayCommand<Window> LoginCommand
        {
            get
            {
                return new RelayCommand<Window>((res) =>
                {
                    if (AppData.CurrentUser.username != null&&AppData.CurrentUser.password!=null)
                    {
                        OrCale orcale = new OrCale();
                        string cmduser = string.Format("SELECT * FORM \"User\" WHERE username='{0}' and password='{1}'", AppData.CurrentUser.username, AppData.CurrentUser.password);
                        DataTable data = orcale.Query(cmduser).Tables[0];
                        if (data.Rows.Count == 0)
                        {
                            MessageBox.Show("用户名或密码错误");
                            return;
                        }
                        res.Close();
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                    }
                });
            }
        }
    }
}
