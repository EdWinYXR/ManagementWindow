using System.Collections.Generic;
using ManagementWindow.BaseClass;
using ManagementWindow.SQL;
using ManagementWindow.View;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace ManagementWindow.ViewModel
{
    public class AddUserViewModel : ObservableObject
    {
        public AddUserViewModel()
        {
            CurrentUser = SqlAssociated.CmdAllUserGetAddUserWindow();
        }
        public AppData AppData { get; set; } = AppData.Instance;

        public List<CurrentUser> CurrentUser { get; set; } = null;


        /// <summary>
        ///增加用户
        /// </summary>
        public RelayCommand AddUser
        {
            get
            {
                return new RelayCommand(() =>
                {
                    CurrentUser us = new CurrentUser
                    {
                        username = AppData.AddUserWindow.textBoxUserName.Text.Trim().ToString(),
                        password = AppData.AddUserWindow.textpassword.Text.Trim().ToString(),
                        level = AppData.AddUserWindow.Department.Text.ToString()
                    };
                    int i = SqlAssociated.AddUserFromAdduserwindow(us);
                    if (i != 0)
                    {
                        CurrentUser = SqlAssociated.CmdAllUserGetAddUserWindow();
                        AppData.MainWindow.container.Content = new AddUserWindow();
                    }
                });
            }
        }
    }
}
