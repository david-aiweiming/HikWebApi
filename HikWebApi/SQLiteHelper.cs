using Microsoft.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HikWebApi
{
    /// <summary>
    /// 数据访问基础类(基于SQLite)
    /// 可以用户可以修改满足自己项目的需要。
    /// </summary>
    public abstract class SQLiteHelper
    {
        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.
        public static string connectionString = CreateConnectionString();
        public SQLiteHelper()
        {
        }
        private static string CreateConnectionString()
        {

            string dbName = "taotian.db";
            string sqlLitePath = "data source=" + AppDomain.CurrentDomain.BaseDirectory + "SqlLiteDB\\" + dbName + ";";
            return sqlLitePath;
        }

        #region 公用方法

        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        public static bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //public static bool Exists(string strSql, params SqliteParameter[] cmdParms)
        //{
        //    object obj = GetSingle(strSql, cmdParms);
        //    int cmdresult;
        //    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
        //    {
        //        cmdresult = 0;
        //    }
        //    else
        //    {
        //        cmdresult = int.Parse(obj.ToString());
        //    }
        //    if (cmdresult == 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        #endregion

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                using (SqliteCommand cmd = new SqliteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqliteException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static void ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (SqliteConnection conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = conn;
                SqliteTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (SqliteException ex)
                {
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        //public static int ExecuteSql(string SQLString, string content)
        //{
        //    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        //    {
        //        SQLiteCommand cmd = new SQLiteCommand(SQLString, connection);
        //        SQLiteParameter myParameter = new SQLiteParameter("@content", DbType.String);
        //        myParameter.Value = content;
        //        cmd.Parameters.Add(myParameter);
        //        try
        //        {
        //            connection.Open();
        //            int rows = cmd.ExecuteNonQuery();
        //            return rows;
        //        }
        //        catch (System.Data.SQLite.SQLiteException E)
        //        {
        //            throw new Exception(E.Message);
        //        }
        //        finally
        //        {
        //            cmd.Dispose();
        //            connection.Close();
        //        }
        //    }
        //}
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        //public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        //{
        //    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        //    {
        //        SQLiteCommand cmd = new SQLiteCommand(strSQL, connection);
        //        SQLiteParameter myParameter = new SQLiteParameter("@fs", DbType.Binary);
        //        myParameter.Value = fs;
        //        cmd.Parameters.Add(myParameter);
        //        try
        //        {
        //            connection.Open();
        //            int rows = cmd.ExecuteNonQuery();
        //            return rows;
        //        }
        //        catch (System.Data.SQLite.SQLiteException E)
        //        {
        //            throw new Exception(E.Message);
        //        }
        //        finally
        //        {
        //            cmd.Dispose();
        //            connection.Close();
        //        }
        //    }
        //}

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                using (SqliteCommand cmd = new SqliteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SqliteException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回SQLiteDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SQLiteDataReader</returns>
        //public static SQLiteDataReader ExecuteReader(string strSQL)
        //{
        //    SqliteConnection connection = new SqliteConnection(connectionString);
        //    SqliteCommand cmd = new SqliteCommand(strSQL, connection);
        //    try
        //    {
        //        connection.Open();
        //        SqliteDataReader myReader = cmd.ExecuteReader();
        //        return myReader;
        //    }
        //    catch (SqliteException e)
        //    {
        //        throw new Exception(e.Message);
        //    }

        //}
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataTable Query(string SQLString)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                DataTable dataTable = new DataTable();
                try
                {
                    connection.Open();
                    using (SqliteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = SQLString;
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {

                            dataTable.Load(reader);

                        }
                    }
                }
                catch (SqliteException ex)
                {
                    throw new Exception(ex.Message);
                }
                return dataTable;
            }
        }



        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params SqliteParameter[] cmdParms)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                using (SqliteCommand cmd = new SqliteCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (SqliteException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }




        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params SqliteParameter[] cmdParms)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                using (SqliteCommand cmd = new SqliteCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SqliteException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }



        private static void PrepareCommand(SqliteCommand cmd, SqliteConnection conn, SqliteTransaction trans, string cmdText, SqliteParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqliteParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion



    }
}


