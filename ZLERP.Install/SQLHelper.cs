using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZLERP.Install
{
    public class SQLHelper
    {
        private string CONNECTION_STRING;
        private SqlConnection conn;
        private const string m_MasterDataBase = "master";
        public SQLHelper(string server, string user, string pwd)
        {
            this.CONNECTION_STRING = string.Format("Data Source={0};User ID={1};Password={2}", server, user, pwd);
            conn = new SqlConnection(this.CONNECTION_STRING);
        }

        bool OpenConnection()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            return true;
        }

        public bool IsDBExists(string name)
        {
            SqlCommand cmd = new SqlCommand(string.Format("SELECT NAME FROM SYSDATABASES WHERE NAME ='{0}'", name), conn);
            if (OpenConnection())
            {
                try
                {
                    cmd.Connection.ChangeDatabase(m_MasterDataBase);
                    return cmd.ExecuteScalar() != null;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbName"></param>
        /// <param name="splitWith"></param>
        /// <returns></returns>
        public int ExecuteNoneQueryNoTrans(string sql, string dbName, string splitWith)
        {
            int result = 0;

            SqlCommand cmd = new SqlCommand(sql, conn);
            OpenConnection();
            cmd.Connection.ChangeDatabase(dbName);

            cmd.CommandType = CommandType.Text;

            string[] sqlList;
            if (!string.IsNullOrEmpty(splitWith))
                sqlList = sql.Split(new string[] { splitWith }, StringSplitOptions.RemoveEmptyEntries);
            else
                sqlList = new string[] { sql };
            try
            {
                foreach (string s in sqlList)
                {
                    cmd.CommandText = s;
                    result += cmd.ExecuteNonQuery();
                }


            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbName"></param>
        /// <param name="splitWith"></param>
        /// <returns></returns>
        public int ExecuteNoneQuery(string sql, string dbName, string splitWith)
        {
            int result = 0;

            SqlCommand cmd = new SqlCommand(sql, conn);
            OpenConnection();
            cmd.Connection.ChangeDatabase(dbName);
            SqlTransaction trans = conn.BeginTransaction();
            cmd.CommandType = CommandType.Text;
            cmd.Transaction = trans;
            string[] sqlList;
            if (!string.IsNullOrEmpty(splitWith))
                sqlList = sql.Split(new string[] { splitWith }, StringSplitOptions.RemoveEmptyEntries);
            else
                sqlList = new string[] { sql };
            try
            {
                foreach (string s in sqlList)
                {
                    cmd.CommandText = s;
                    result += cmd.ExecuteNonQuery();
                }

                trans.Commit();
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return result;
        }

        public void ExecuteSql(string DatabaseName, string Sql)
        {
            SqlCommand cmd = new SqlCommand(Sql, conn);
            OpenConnection();
            conn.ChangeDatabase(DatabaseName);
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }



        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool CreateDatabase(string dbName, string filePath)
        {
            string sql = "CREATE DATABASE [{0}] ON (NAME = N'{0}_Data', FILENAME = N'{1}\\{0}_Data.MDF' , FILEGROWTH = 10%) "
                       + "LOG ON (NAME = N'{0}_Log', FILENAME = N'{1}\\{0}_Log.LDF' , FILEGROWTH = 10%) "
                       + "COLLATE Chinese_PRC_CI_AS";

            SqlCommand cmd = new SqlCommand(string.Format(sql, dbName, filePath), conn);
            if (OpenConnection())
            {
                try
                {
                    cmd.Connection.ChangeDatabase(m_MasterDataBase);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                    //return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            else
                return false;
        }

        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public bool DropDatabase(string dbName)
        {
            SqlCommand cmd = new SqlCommand(string.Format("DROP DATABASE {0}", dbName), conn);
            if (OpenConnection())
            {
                try
                {
                    cmd.Connection.ChangeDatabase(m_MasterDataBase);
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            else
                return false;

        }
    }
}
