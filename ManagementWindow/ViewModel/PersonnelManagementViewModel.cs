using ManagementWindow.BaseClass;
using ManagementWindow.SQL;
using ManagementWindow.View;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SQL;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ManagementWindow.ViewModel
{
    public class PersonnelManagementViewModel : ObservableObject
    {
        public AppData AppData { get; set; } = AppData.Instance;
        public RelayCommand<ListView> DeletePersonnel
        {
            get
            {
                return new RelayCommand<ListView>((res) =>
                {
                    Staff staff = res.SelectedItem as Staff;
                    if (MessageBox.Show(string.Format("是否删除 {0} 的员工信息", staff.Name), "警告", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        int i = SqlAssociated.DeletePersonnelFromPersonnelManagement(staff);
                        if (i != 0)
                        {
                            AppData.MainWindow.container.Content = new PersonnelManagement();
                        }
                    }
                    else
                    {
                        return;
                    }
                });
            }
        }

        public RelayCommand AddPersonnel
        {
            get
            {
                return new RelayCommand(() =>
                {
                    View.AddPersonnel add = new View.AddPersonnel();
                    add.ShowDialog();
                });
            }
        }

        public RelayCommand<ListView> BindingProject
        {
            get
            {
                return new RelayCommand<ListView>((res) =>
                {
                    try
                    {
                        Staff staff = res.SelectedItem as Staff;
                        BindingProjectWindow bin = new BindingProjectWindow();
                        if (bin.ShowDialog() == true)
                        {
                            string sta = DateTime.Now.ToString("yyyy/MM/dd");
                            string end = DateTime.Now.ToString("yyyy/MM/dd");
                            if (AppData.PersonnelManagement.starte.SelectedDate != null)
                            {
                                sta = ((DateTime)AppData.PersonnelManagement.starte.SelectedDate).ToString("yyyy/MM/dd");
                            }
                            if (AppData.PersonnelManagement.end.SelectedDate != null)
                            {
                                end = ((DateTime)AppData.PersonnelManagement.end.SelectedDate).ToString("yyyy/MM/dd");
                            }
                            if (
                            MessageBox.Show(string.Format("请确认绑定时间为{0}--{1}", sta, end),
                               "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                string Binding = string.Format("CALL \"Pro_BindingItem\"('{0}','{1}','{2}','{3}')",
                                        AppData.BindingProjectWindowViewModel.ItemNo,
                                        staff.ID,
                                        sta,
                                       end);
                                OrCale orCale = new OrCale();
                                orCale.Change(Binding);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string s = ex.Message;
                    }
                });
            }
        }
    }
}
