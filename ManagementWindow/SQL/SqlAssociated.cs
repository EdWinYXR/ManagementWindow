using ManagementWindow.BaseClass;
using SQL;
using System.Collections.Generic;
using System.Data;

namespace ManagementWindow.SQL
{
    public class SqlAssociated
    {
      public static OrCale orCale = new OrCale();

        public static List<ItemMes> CmdItemMESGetUI()
        {
            List<ItemMes> L_mes = new List<ItemMes>();
            string cmditem = "SELECT * FROM \"ItemManagement\"  WHERE 'ItemState'!='5' ";
            DataSet data = orCale.Query(cmditem);
            if (data.Tables[0].Rows.Count != 0)
            {
                foreach(DataRow row in data.Tables[0].Rows)
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
    }
}
