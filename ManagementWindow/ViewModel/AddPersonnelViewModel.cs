using ManagementWindow.View;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ManagementWindow.ViewModel
{
   public  class AddPersonnelViewModel : ObservableObject
    {
        public AppData AppData = AppData.Instance;
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
        /// 增加人员信息
        /// </summary>
        public RelayCommand<Window> AddP
        {
            get
            {
                return new RelayCommand<Window>((res) =>
                {
                    try
                    {
                        OrCale orCale = new OrCale();
                        string AddPersonnel = string.Format("Call ProAddStaff({0},{1},{2},{3},{4})", AppData.AddPersonnel.ID.Text,
                            AppData.AddPersonnel.Name.Text,
                            AppData.AddPersonnel.Email.Text,
                            AppData.AddPersonnel.Phone.Text,
                            AppData.AddPersonnel.Department.Text);
                       int i= orCale.Change(AddPersonnel);
                        if (i != 0)
                        {
                            res.Close();
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("添加失败：" + ex.Message);
                    }
                });
            }
        }
    }
}
