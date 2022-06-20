using ManagementWindow.BaseClass;
using ManagementWindow.View;
using ManagementWindow.ViewModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Net;
using System;
using System.IO;

namespace ManagementWindow
{
    public class AppData : ObservableObject
    {
        public  AppData()
        {
            FontFamilyAll = "黑体";
        }
        public static AppData Instance = new Lazy<AppData>(() => new AppData()).Value;
        /// <summary>
        /// 字体设置
        /// </summary>
        private string fontFamilyAll;
        public string FontFamilyAll { get => fontFamilyAll; set => SetProperty(ref fontFamilyAll, value); }

        /// <summary>
        /// 用户名称
        /// </summary>
        private CurrentUser currentUser=new CurrentUser();
        public CurrentUser CurrentUser { get => currentUser; set => SetProperty(ref currentUser, value); }
        #region 执行期向下文
        public MainWindow MainWindow { get; set; } = null;
        public Login LoginModel { get; set; } = null;
        public AddPersonnel AddPersonnel { get; set; } = null;
        // public BindingProjectWindow BindingProjectWindow { get; set; } = null;
        public BindingProjectWindowViewModel BindingProjectWindowViewModel { get; set; } = null;
        public BindingItemViewModel BindingItemViewModel { get; set; } = null;
        public AddItems AddItems { get; set; } = null;
        public AddUserWindow AddUserWindow { get; set; } = null;
        public PersonnelManagement PersonnelManagement { get; set; } = null;
        #endregion
    }
}
