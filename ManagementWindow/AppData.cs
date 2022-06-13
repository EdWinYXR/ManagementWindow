using ManagementWindow.BaseClass;
using ManagementWindow.View;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using SQL;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementWindow
{
    public class AppData : ObservableObject
    {
        public AppData()
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
        public BindingProjectWindow BindingProjectWindow { get; set; } = null;
        #endregion
    }
}
