using ManagementWindow.BaseClass;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementWindow.ViewModel
{
    public class BindingProjectWindowViewModel : ObservableObject
    {
       public  AppData AppData { get; set; } = AppData.Instance;
       
        public string ItemNo { get; set; }
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="mes">选中项</param>
        public void DoubleClickBinding(ItemMes mes)
        {
            ItemNo = mes.ItemNo;
            //string Binding = string.Format("CALL \"Pro_BindingItem\"('{0}','{1}','{2}','{3}')",mes.ItemNo);
        }
    }
}
