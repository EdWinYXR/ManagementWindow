using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;

namespace SQL
{
    public class OrCale
    {
        //数据源
        readonly String ConString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.3.114)(PORT=1521))(CONNECT_DATA=(SID=ProPerDBSid)));Persist Security Info=True;User ID=MES;Password=qwasZX12;";
        OracleConnection conn = null;
        private static object obj = new object();//声明锁的类型

        //打开SQL
        public void connstart()
        {
            conn = new OracleConnection(ConString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

        }

        //关闭SQL
        public void connclose()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        //查询
        public DataSet Query(string sql)
        {
            DataSet ds = new DataSet();
            lock (obj)
            {
                connstart();
                try
                {
                    OracleDataAdapter da = new OracleDataAdapter(sql, conn);
                    da.Fill(ds);
                    connclose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //CNetLog.Instance.WriteLog("MySQL CMD Fail :" + ex.Message);
                }
                return ds;
            }
        }

        //更改
        public int Change(string sql)
        {
            lock (obj)
            {
                connstart();
                int x = 0;
                try
                {
                    //MySqlCommand oc = new MySqlCommand();//表示要对数据源执行的SQL语句或储存过程
                    OracleCommand oc = new OracleCommand(sql, conn);
                    oc.CommandText = sql;//设置命令的文本 
                    oc.CommandType = CommandType.Text;//设置命令的类型
                    oc.Connection = conn;//设置命令的连接
                    x = oc.ExecuteNonQuery();//执行SQL语句
                    connclose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //CNetLog.Instance.WriteLog("MySQL Change Fail :" + ex.Message);
                }
                return x;
            }
        }
        /// <summary>
        ///调用存储过程
        /// </summary>
        /// <param name="strProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns></returns>
        public DataSet QueryPro(string strProcName, OracleParameter[] parameters)
        {

            DataSet dt = new DataSet();
            lock (obj)
            {
                connstart();
                try
                {
                    OracleCommand cmd = new OracleCommand(strProcName,conn);
                    cmd.CommandText = strProcName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    if (parameters != null)
                    {
                        // 添加参数
                        cmd.Parameters.AddRange(parameters);
                    }
                    // 取数据
                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {

                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误:" + ex.Message + "/r/n跟踪:" + ex.StackTrace);
                }
                finally
                {
                    connclose();
                }
                return dt;
            }
        }
    }
}

