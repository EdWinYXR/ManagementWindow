using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Net;
using SQL;
using System.Data;
using System.Windows;

namespace ManagementWindow.ViewModel
{
    public class LoginWindowViewModel : ObservableObject
    {
        public LoginWindowViewModel()
        {

        }
        public AppData AppData { get; set; } = AppData.Instance;
        /// <summary>
        /// 登录
        /// </summary>
        public RelayCommand<Window> LoginCommand
        {
            get
            {
                return new RelayCommand<Window>((res) =>
                {
                    if (AppData.CurrentUser.username != null && AppData.LoginModel.password.Password!=null)
                    {
                        OrCale orcale = new OrCale();
                        string cmduser = string.Format("SELECT * FROM  MES.\"User\" WHERE \"username\"='{0}' and \"password\"='{1}'", AppData.CurrentUser.username, AppData.LoginModel.password.Password);
                        DataSet data = orcale.Query(cmduser);
                        if (data.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("用户名或密码错误");
                            return;
                        }
                        AppData.CurrentUser.level = ConverterLevel(data.Tables[0].Rows[0].Field<decimal>("Level"));
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        CNetLog.Instance.WriteLog( "Open System started successfully");
                        res.Close();
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码不能为空");
                    }
                });
            }
        }

        public string ConverterLevel(decimal i)
        {
            string level = "";
            switch (i) {
                case 1:
                     level = "超级管理员";
                    break;
                case 2:
                    level = "管理员";
                    break;
                case 3:
                    level = "用户";
                    break;
            }
            return level;
        }
        public RelayCommand<Window> CloseCommand
        {
            get
            {
                return new RelayCommand<Window>((res) =>
                {
                    res.Close();
                });
            }
        }

    }
}
