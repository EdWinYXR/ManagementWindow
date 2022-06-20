using ManagementWindow.BaseClass;
using Oracle.ManagedDataAccess.Client;
using SQL;
using System;
using System.Collections.Generic;
using System.Data;
using Net;
using System.Reflection;

namespace ManagementWindow.SQL
{
    public class SqlAssociated
    {
        public static OrCale orCale = new OrCale();

        public static List<ItemMes> CmdItemMESGetUI()
        {
            try
            {
                List<ItemMes> L_mes = new List<ItemMes>();
                string cmditem = "SELECT * FROM \"ItemManagement\"  WHERE 'ItemState'!='5' ";
                DataSet data = orCale.Query(cmditem);
                if (data.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        ItemMes mes = new ItemMes();
                        if (row["ItemState"] != null)
                        {
                            switch (row["ItemState"].ToString())
                            {
                                case "1":
                                    mes.ItemState = "等待";
                                    break;
                                case "2":
                                    mes.ItemState = "施工";
                                    break;
                                case "3":
                                    mes.ItemState = "调试";
                                    break;
                                case "4":
                                    mes.ItemState = "验收";
                                    break;
                                default:
                                    continue;
                            }
                        }
                        if (row["ItemName"] != null)
                        {
                            mes.ItemName = row["ItemName"].ToString();
                        }
                        if (row["ItemNo"] != null)
                        {
                            mes.ItemNo = row["ItemNo"].ToString();
                        }
                        if (row["StartTime"] != null)
                        {
                            mes.StartTime = row["StartTime"].ToString();
                        }
                        if (row["EndTime"] != null)
                        {
                            mes.EndTime = row["EndTime"].ToString();
                        }
                        if (row["Participants"] != null)
                        {
                            mes.Participants = row["Participants"].ToString();
                        }
                        L_mes.Add(mes);
                    }
                }
                return L_mes;
            }
            catch (Exception ex)
            {
                CNetLog.Instance.WriteLog(MethodBase.GetCurrentMethod().Name + ",FAIL Because：" + ex.Message);
            }
            return null;
        }

        public static List<ItemMes> CmdAllItemMESGetUI()
        {
            try
            {
                List<ItemMes> L_mes = new List<ItemMes>();
                string cmditem = "SELECT * FROM \"ItemManagement\" ";
                DataSet data = orCale.Query(cmditem);
                if (data.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        ItemMes mes = new ItemMes();
                        if (row["ItemState"] != null)
                        {
                            switch (row["ItemState"].ToString())
                            {
                                case "1":
                                    mes.ItemState = "等待";
                                    break;
                                case "2":
                                    mes.ItemState = "施工";
                                    break;
                                case "3":
                                    mes.ItemState = "调试";
                                    break;
                                case "4":
                                    mes.ItemState = "验收";
                                    break;
                                default:
                                    mes.ItemState = "完成";
                                    break;
                            }
                        }
                        if (row["ItemName"] != null)
                        {
                            mes.ItemName = row["ItemName"].ToString();
                        }
                        if (row["ItemNo"] != null)
                        {
                            mes.ItemNo = row["ItemNo"].ToString();
                        }
                        if (row["StartTime"] != null)
                        {
                            mes.StartTime = row["StartTime"].ToString();
                        }
                        if (row["EndTime"] != null)
                        {
                            mes.EndTime = row["EndTime"].ToString();
                        }
                        if (row["Participants"] != null)
                        {
                            mes.Participants = row["Participants"].ToString();
                        }
                        L_mes.Add(mes);
                    }
                }
                return L_mes;
            }
            catch (Exception ex)
            {
                CNetLog.Instance.WriteLog(MethodBase.GetCurrentMethod().Name + ",FAIL Because：" + ex.Message);
            }
            return null;
        }

        public static int DeleteItemFormUI_ItemManagement(ItemMes mes)
        {
            try
            {
                string delete = "DELETE FROM \"ItemManagement\" WHERE 'ItemNo'='" + mes.ItemNo + "'";
                return orCale.Change(delete);
            }
            catch (Exception ex)
            {
                CNetLog.Instance.WriteLog(MethodBase.GetCurrentMethod().Name + ",FAIL Because：" + ex.Message);
            }
            return 0;
        }

        public static List<CurrentUser> CmdAllUserGetAddUserWindow()
        {
            try
            {
                List<CurrentUser> listcurrent = new List<CurrentUser>();
                string cmdall = "SELECT * FROM \"User\"";
                DataSet data = orCale.Query(cmdall);
                if (data.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        CurrentUser user = new CurrentUser();
                        if (row["username"] != null)
                        {
                            user.username = row["username"].ToString();
                        }
                        if (row["password"] != null)
                        {
                            user.password = row["password"].ToString();
                        }
                        if (row["level"] != null)
                        {
                            user.level = row["level"].ToString();
                        }
                        listcurrent.Add(user);
                    }
                }
                return listcurrent;
            }
            catch (Exception ex)
            {
                CNetLog.Instance.WriteLog(MethodBase.GetCurrentMethod().Name + ",FAIL Because：" + ex.Message);
            }
            return null;
        }

        public static int AddUserFromAdduserwindow(CurrentUser user)
        {
            try
            {
                switch (user.level)
                {
                    case "用户":
                        user.level = "3";
                        break;
                    case "管理员":
                        user.level = "2";
                        break;
                    case "超级管理员":
                        user.level = "1";
                        break;
                }
                string add = string.Format("INSERT INTO \"User\"(\"username\",\"password\",\"Level\") VALUES('{0}','{1}','{2}')", user.username,
                    user.password,
                    user.level);
                return orCale.Change(add);
            }
            catch (Exception ex)
            {
                CNetLog.Instance.WriteLog(MethodBase.GetCurrentMethod().Name + ",FAIL Because：" + ex.Message);
            }
            return 0;
        }

        public static int DeletePersonnelFromPersonnelManagement(Staff staff)
        {
            try
            {
                string de = "DELETE FROM \"Staff\" WHERE ID='" + staff.ID + "'";
                return orCale.Change(de);
            }
            catch (Exception ex)
            {
                CNetLog.Instance.WriteLog(MethodBase.GetCurrentMethod().Name + ",FAIL Because：" + ex.Message);
            }
            return 0;
        }

        public static List<Staff> CmdAllPersonndelGetUI(string starttime, string endtime)
        {
            try
            {
                List<Staff> L_listStaff = new List<Staff>();
                string cmdall = "SELECT * FROM \"Staff\" ";
                DataSet data = orCale.Query(cmdall);
                if (data.Tables[0].Rows.Count == 0)
                {
                    return L_listStaff;
                }
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    Staff staff = new Staff();
                    if (row["ID"] != null)
                    {
                        staff.ID = row["ID"].ToString();
                        string cmdnum = "SELECT COUNT(*) ItemNum FROM \"ItemStaff\" WHERE \"ID\"='" + staff.ID + "'";
                        if (starttime != "" && endtime != "")
                        {
                            cmdnum = string.Format("SELECT COUNT(*) ItemNum FROM \"ItemStaff\" WHERE \"ID\"='{0}' AND  \"StarTime\" >= '{1}' AND \"EndTime\" <= '{2}'",
                                                                     staff.ID, starttime, endtime);
                        }
                        else if (starttime != "")
                        {
                            cmdnum = string.Format("SELECT COUNT(*) ItemNum FROM \"ItemStaff\" WHERE \"ID\"='{0}' AND  \"StarTime\" >= '{1}'",
                                                                    staff.ID, starttime);
                        }
                        else if (endtime != "")
                        {
                            cmdnum = string.Format("SELECT COUNT(*) ItemNum FROM \"ItemStaff\" WHERE \"ID\"='{0}'  AND \"EndTime\" <= '{1}'",
                                                                     staff.ID, endtime);
                        }
                        else
                        {
                            cmdnum = "SELECT COUNT(*) ItemNum FROM \"ItemStaff\" WHERE \"ID\"='" + staff.ID + "'";
                        }
                        staff.ItemNum = orCale.Query(cmdnum).Tables[0].Rows[0][0].ToString();
                    }
                    if (row["Name"] != null)
                    {
                        staff.Name = row["Name"].ToString();
                    }
                    if (row["Email"] != null)
                    {
                        staff.Email = row["Email"].ToString();
                    }
                    if (row["Phone"] != null)
                    {
                        staff.Phone = row["Phone"].ToString();
                    }
                    if (row["Department"] != null)
                    {
                        staff.Department = row["Department"].ToString();
                    }
                    if (row["grade"] != null)
                    {
                        switch (row["grade"].ToString())
                        {
                            case "1":
                                staff.grade = "一级";
                                break;
                            case "2":
                                staff.grade = "二级";
                                break;
                            case "3":
                                staff.grade = "三级";
                                break;
                            case "4":
                                staff.grade = "四级";
                                break;
                            case "5":
                                staff.grade = "五级";
                                break;
                            default:
                                staff.grade = "一级";
                                break;
                        }
                    }
                    L_listStaff.Add(staff);
                }
                return L_listStaff;
            }
            catch (Exception ex)
            {
                CNetLog.Instance.WriteLog(MethodBase.GetCurrentMethod().Name + ",FAIL Because：" + ex.Message);
            }
            return null;
        }

        public static int AddPersonnelFormPersonndelManagement(Staff staff)
        {
            try
            {
                switch (staff.grade)
                {
                    case "一级":
                        staff.grade = "1";
                        break;
                    case "二级":
                        staff.grade = "2";
                        break;
                    case "三级":
                        staff.grade = "3";
                        break;
                    case "四级":
                        staff.grade = "4";
                        break;
                    case "五级":
                        staff.grade = "5";
                        break;

                }
                string add = string.Format("INSERT INTO \"Staff\"(\"ID\",\"Name\",\"Email\",\"Phone\",\"Department\",\"grade\") VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}'); ", staff.ID,
                    staff.Name,
                    staff.Email,
                    staff.Phone,
                    staff.Department,
                    staff.grade);
                return orCale.Change(add);
            }
            catch (Exception ex)
            {
                CNetLog.Instance.WriteLog(MethodBase.GetCurrentMethod().Name + ",FAIL Because：" + ex.Message);
            }
            return 0;
        }

        public static int AddItemStaffFromBindingItemWindow(ItemStaff item)
        {
            int i = 0;
            try
            {
                string Binding = string.Format("CALL \"Pro_BindingItem\"('{0}','{1}','{2}','{3}')",
                           item.ItemNo,
                           item.ID,
                           item.Starttime,
                          item.EndTime);
                orCale.Change(Binding);
                i = 1;
            }
            catch (Exception ex)
            {
                CNetLog.Instance.WriteLog(MethodBase.GetCurrentMethod().Name + ",FAIL Because：" + ex.Message);
            }
            return 0;
        }

        public static int AddItemsFromAddPersonnelViewModel(ItemMes item)
        {
            try
            {
                string Additems = string.Format("INSERT INTO \"ItemManagement\"(\"ItemNo\",\"ItemName\",\"StartTime\",\"EndTime\",\"Department\") VALUES('{0}','{1}','{2}','{3}','{4}')",
                               item.ItemNo,
                              item.ItemName,
                               item.StartTime,
                              item.EndTime,
                               item.Department);
                return orCale.Change(Additems);
            }
            catch (Exception ex)
            {
                CNetLog.Instance.WriteLog(MethodBase.GetCurrentMethod().Name + ",FAIL Because：" + ex.Message);
            }
            return 0;
        }
    }
}
