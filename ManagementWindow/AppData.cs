using Microsoft.Toolkit.Mvvm.ComponentModel;
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
        private string userName;
        public string UserName { get => userName; set => SetProperty(ref userName, value); }
        /// <summary>
        /// 用户权限
        /// </summary>
        private string userPermissions;
        public string UserPermissions { get => userPermissions; set => SetProperty(ref userPermissions, value); }

        public MainWindow MainWindow { get; set; } = null;
    }
}
