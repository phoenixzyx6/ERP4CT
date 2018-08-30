using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.IO.Compression;
using System.Data;
using ZLERP.Model.ViewModels;

namespace ZLERP.Web.Helpers
{
    public class SqlServerHelper
    {
        public static string m_ConnStr = ConfigurationManager.ConnectionStrings["ZLERP"].ConnectionString;
        /// <summary>
        /// 取得数据表占用空间信息
        /// </summary>
        /// <returns></returns>
        public IList<DBTableInfo> GetTablesInfo()
        {
             
            //SqlCommand comm = new SqlCommand();

            //comm.Connection = conn;
            //comm.CommandText = "CREATE TABLE #tableSpaceUsed ( 	Name VARCHAR(50), 	[Rows] INT, 	Reserved VARCHAR(20), 	Data VARCHAR(20), 	IndexSize VARCHAR(20), 	UnUsed VARCHAR(20) ) insert into #tableSpaceUsed exec sp_MSforeachtable \"sp_spaceused '?'\"  SELECT t.*, p.value FROM #tableSpaceUsed t LEFT OUTER JOIN (SELECT  OBJECT_NAME(id) AS NAME, value FROM sysproperties WHERE  smallid=0 AND  name='MS_Description')p ON t.Name = p.NAME order by t.Name;  DROP TABLE #tableSpaceUsed";

            //conn.Open();
            //string sql = "CREATE TABLE #tableSpaceUsed ( 	Name VARCHAR(50), 	[Rows] INT, 	Reserved VARCHAR(20), 	Data VARCHAR(20), 	IndexSize VARCHAR(20), 	UnUsed VARCHAR(20) ) insert into #tableSpaceUsed exec sp_MSforeachtable \"sp_spaceused '?'\"  SELECT t.*, p.value FROM #tableSpaceUsed t LEFT OUTER JOIN (SELECT  OBJECT_NAME(id) AS NAME, value FROM sysproperties WHERE  smallid=0 AND  name='MS_Description')p ON t.Name = p.NAME order by t.Name;  DROP TABLE #tableSpaceUsed";
            string sql = "sp_sql_tables_size";
            using (SqlDataReader sdr = ExecuteReader(sql, CommandType.StoredProcedure, null))
            {
                IList<DBTableInfo> tables = new List<DBTableInfo>();

                while (sdr.Read())
                {

                    DBTableInfo t = new DBTableInfo();
                    t.Name = sdr.GetString(0);
                    t.Rows = sdr.GetInt32(1);
                    t.Reserved = sdr.GetString(2);
                    t.Data = sdr.GetString(3);
                    t.IndexSize = sdr.GetString(4);
                    t.UnUsed = sdr.GetString(5);
                    t.Desc = sdr.IsDBNull(6) ? "" : sdr.GetString(6);
                    tables.Add(t);
                }
                return tables;
            }

        }
        /// <summary>
        /// 取得数据库文件信息
        /// </summary>
        /// <returns></returns>
        public IList<dynamic> GetDBFileInfo()
        {
            DataSet ds = ExecuteDataset("sp_helpfile", CommandType.StoredProcedure, null);
            
            IList<dynamic> data = new List<dynamic>();
            if (ds != null && ds.Tables.Count > 0) {
                foreach (DataRow row in ds.Tables[0].Rows) {
                    data.Add(new {
                                Name=row["name"].ToString().Trim(),
                                  FileName = row["filename"].ToString().Trim(),
                                  Size = row["size"].ToString().Trim(),
                                  MaxSize = row["maxsize"].ToString().Trim()
                    });
                }
            }
            return data;
        }

        
        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="compress"></param>
        /// <param name="backupPath">备份文件绝对路径</param>
        /// <returns></returns>
        public bool BackupDB(string fileName, bool compress, string backupPath)
        {　
            backupPath = Path.Combine(backupPath, fileName);
            backupPath += DateTime.Now.ToString("yyyyMMddHmmss.bak");
            using (SqlConnection conn = new SqlConnection(m_ConnStr))
            {
                try
                {
                    conn.Open(); 
                    string bakSql = "BACKUP DATABASE [{0}] TO DISK='{1}' WITH NOFORMAT, INIT,  NAME = N'{2}', SKIP";
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = string.Format(bakSql, conn.Database, backupPath, fileName);
                    comm.ExecuteNonQuery();
                    if (compress)
                    {
                        string zipFile = Path.Combine(Path.GetDirectoryName(backupPath), Path.GetFileNameWithoutExtension(backupPath) + ".zip");

                        if(ZipHelper.ZipFile(backupPath, zipFile))
                        {
                            //删除原bak文件
                            File.Delete(backupPath);
                            return true;
                        }
                        else
                            return false;

                    }
                    return true;

                } 
                finally
                {
                    conn.Close();
                }
            }


        }



        #region Helper Method
        public SqlDataReader ExecuteReader(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(cmdText, CommandType.Text, commandParameters);
        }

        public SqlDataReader ExecuteReader(string cmdText,CommandType cmdType, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(m_ConnStr);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        /// <summary>
        /// 返回dataset
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string cmdText, CommandType type, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(m_ConnStr);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                PrepareCommand(cmd, conn, null, type, cmdText, commandParameters);
                sda.Fill(ds, "table");
                cmd.Parameters.Clear();
                return ds;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行无返回值的查询
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters) {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(m_ConnStr);
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        private void PrepareCommand(SqlCommand cmd, SqlConnection connection, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (connection.State != ConnectionState.Open)
                connection.Open();

            cmd.Connection = connection;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion


    }
}