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
    public class AddPersonnelViewModel : ObservableObject
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
                    catch (Exception ex)
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
                        ItemMes item = new ItemMes
                        {
                            ItemNo = AppData.AddItems.ItemNo.Text,
                            ItemName = AppData.AddItems.ItemName.Text,
                            StartTime = (AppData.AddItems.StartTime.SelectedDate ?? DateTime.Now).ToString("yyyy/MM/dd"),
                            EndTime = (AppData.AddItems.StartTime.SelectedDate ?? DateTime.Now).ToString("yyyy/MM/dd"),
                            Department = ConversionCombox(AppData.AddItems.Department.Text)
                        };
                        int i = SqlAssociated.AddItemsFromAddPersonnelViewModel(item);
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


        public int ConversionCombox(string com)
        {
            switch (com)
            {
                case "自动化":
                    return 1;
                case "智能":
                    return 2;
                case "弘享":
                    return 3;
            }
            return 1;
        }
    }
}
