using ManagementWindow.BaseClass;
using ManagementWindow.SQL;
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
                        Staff staff = new Staff
                        {
                            ID = AppData.AddPersonnel.ID.Text.Trim().ToString(),
                            Name = AppData.AddPersonnel.Name.Text.Trim().ToString(),
                            Email = AppData.AddPersonnel.Email.Text.Trim().ToString(),
                            Phone = AppData.AddPersonnel.Phone.Text.Trim().ToString(),
                            Department = AppData.AddPersonnel.Department.Text.ToString(),
                            grade = AppData.AddPersonnel.grade.Text.ToString(),
                            ItemNum = "0"
                        };
                        int i = SqlAssociated.AddPersonnelFormPersonndelManagement(staff);
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
        /// <summary>
        /// 增加项目信息
        /// </summary>
        public RelayCommand<Window> AddItem
        {
            get
            {
                return new RelayCommand<Window>((res) =>
                {
                    try
                    {
                        OrCale orCale = new OrCale();
                        string AddPersonnel = string.Format("Call ProAddStaff({0},{1},{2},{3})", AppData.AddItems.ItemNo.Text,
                            AppData.AddItems.ItemName.Text,
                            AppData.AddItems.StartTime.SelectedDate.ToString(),
                            AppData.AddItems.EndTime.SelectedDate.ToString());
                        int i = orCale.Change(AddPersonnel);
                        if (i != 0)
                        {
                            res.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("添加失败：" + ex.Message);
                    }
                });
            }
        }
    }
}
