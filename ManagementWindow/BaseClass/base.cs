using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementWindow.BaseClass
{
    /// <summary>
    /// 用户
    /// </summary>
    public class CurrentUser
    {
        public string username { get; set; }
        public string password { get; set; }
        public string level { get; set; }
    }
    /// <summary>
    /// 员工信息
    /// </summary>
    public class Staff
    {
        public string ID { get; set; }//工号
        public string Name { get; set; }
        public string ItemNum { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }//部门
        public string grade { get; set; }
    }
    /// <summary>
    /// 项目管理
    /// </summary>
    public class ItemMes
    {
        public string ItemName { get; set; }
        public string ItemNo { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ItemState { get; set; }
        public string Participants { get; set; }
    }
    /// <summary>
    /// 人员项目关联
    /// </summary>
    public class ItemStaff
    {
        public string ID { get; set; }
        public string ItemNo { get; set; }
        public string Starttime { get; set; }
        public string EndTime { get; set; }
    }
}
