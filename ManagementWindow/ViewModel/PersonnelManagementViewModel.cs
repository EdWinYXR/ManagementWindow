using ManagementWindow.BaseClass;
using ManagementWindow.SQL;
using ManagementWindow.View;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SQL;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Net;
using BaseClass.Tool;

namespace ManagementWindow.ViewModel
{
    public class PersonnelManagementViewModel : ObservableObject
    {
        public AppData AppData { get; set; } = AppData.Instance;

        private DateTime? starttime;
        public DateTime? Starttime { get => starttime; set => SetProperty(ref starttime, value); }
        private DateTime? endtime;
        public DateTime? Endtime { get => endtime; set => SetProperty(ref endtime, value); }
        /// <summary>
        /// 选择时间
        /// </summary>
        /// <returns></returns>
        public List<Staff> SelectedDateChanged()
        {
            string sta = "";
            string end = "";
            if (Starttime != null)
            {
                sta = ((DateTime)Starttime).ToString("yyyy/MM/dd");
            }
            if (Endtime != null)
            {
                end = ((DateTime)Endtime).ToString("yyyy/MM/dd");
            }
            return SqlAssociated.CmdAllPersonndelGetUI(sta,end);
        }
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
                            CNetLog.Instance.WriteLog("DeletePersonnel：" + JsonHelper<Staff>.GetJsonStr(staff));
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
        /// <summary>
        /// 绑定人员
        /// </summary>
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
                            if (Starttime != null)
                            {
                                sta = ((DateTime)Starttime).ToString("yyyy/MM/dd");
                            }
                            if (Endtime != null)
                            {
                                end = ((DateTime)Endtime).ToString("yyyy/MM/dd");
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
                              int i=  orCale.Change(Binding);
                                if (i != 0)
                                {
                                    AppData.MainWindow.container.Content = new PersonnelManagement();
                                }

                                CNetLog.Instance.WriteLog("BindingProject："+ JsonHelper<Staff>.GetJsonStr(staff));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("绑定失败：" + ex.Message);
                        CNetLog.Instance.WriteLog("BindingProject Fill Because ："+ex.Message);
                    }
                });
            }
        }


    }
}
