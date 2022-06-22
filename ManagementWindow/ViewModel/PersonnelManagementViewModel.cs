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
using LiveCharts;
using LiveCharts.Wpf;

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

        #region 图表
        /// <summary>
        /// 曲线图定义
        /// </summary>
        public SeriesCollection LineSeries { get; set; } = new SeriesCollection();
        public SeriesCollection PieSeries { get; set; } = new SeriesCollection();
        public List<ShowPersonnelManage> mes { get; set; } = SqlAssociated.CmdItemsStaffGetControl();
        /// <summary>
        /// X轴定义
        /// </summary>
        public AxesCollection XAxisX { get; set; } = new AxesCollection();

        //private IChartValues oNTime = new ChartValues<double>();
        //private IChartValues oFFTime = new ChartValues<double>();
        //public IChartValues ONTime { get => oNTime; set => SetProperty(ref oNTime, value); }
        //public IChartValues OFFTime { get => oFFTime; set => SetProperty(ref oFFTime, value); }
        ////ONTime.Add(Convert.ToDouble(work.Days / all.Days));
        ////        OFFTime.Add(Convert.ToDouble((all.Days - work.Days) / all.Days));

        /// <summary>
        /// x轴设置
        /// </summary>
        public  void GetXAxisX()
        {
            XAxisX.Clear();
            var labels = new List<string>();
            if (Starttime != null&& Endtime != null)
            {
                DateTime sta = (DateTime)starttime;
                DateTime end = (DateTime)endtime;
                for (; sta <= end;)
                {
                    sta = sta.AddDays(1);
                    labels.Add(sta.ToString("MM/dd"));
                }
            }
            XAxisX.Add(
                new Axis
                {
                    //坐标值
                    Labels = labels,
                    Separator = new LiveCharts.Wpf.Separator
                    {
                        Step = 1,
                    },
                });
        }
        /// <summary>
        /// 折线图赋值
        /// </summary>
        /// <param name="ID">当前选中的人</param>
        public void LoadingChart(string ID)
        {
            LineSeries.Clear();
            PieSeries.Clear();
            if (Starttime != null && Endtime != null)
            {
                var personnel = mes.Find(e => e.ID == ID);
                DateTime sta = (DateTime)starttime;
                DateTime end = (DateTime)endtime;
                TimeSpan all = end - sta;
                TimeSpan work = Convert.ToDateTime(personnel.EndTime) - Convert.ToDateTime(personnel.StarTime);
                var a =Convert.ToDouble( work.Days) /Convert.ToDouble( all.Days);
                double b = Convert.ToDouble((all.Days - work.Days) / all.Days);
                var res = new LineSeries
                {
                    Title = personnel.Name,
                    Values = new ChartValues<int>(),
                    LineSmoothness = 0
                };

                var pie = new PieSeries
                {
                    Title = "工作时间",
                    Values = new ChartValues<double>
                     {
                       Math.Round(Convert.ToDouble( work.Days) /Convert.ToDouble( all.Days),2)
                     }
                };

                var pie1 = new PieSeries
                {
                    Title = "未安排时间",
                    Values = new ChartValues<double>
                     {
                       Math.Round((Convert.ToDouble(all.Days) - Convert.ToDouble( work.Days)) /Convert.ToDouble(all.Days),2)
                     }
                };
                for (; sta <= end;)
                {
                    sta = sta.AddDays(1);
                    if (Convert.ToDateTime(personnel.StarTime) < sta && sta < Convert.ToDateTime(personnel.EndTime))
                    {
                        res.Values.Add(1);
                    }
                    else
                    {
                        res.Values.Add(0);
                    }
                }

                LineSeries.Add(res);
                PieSeries.Add(pie);
                PieSeries.Add(pie1);
            }
        }

        public void LoadingPieSeries()
        {
            PieSeries.Clear();
        }

        #endregion
    }
}
