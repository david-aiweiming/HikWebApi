
using Microsoft.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HikWebApi.Model.TaoTianHikModel;

namespace HikWebApi.TaoTianDAL
{
    public class TaoTianHikDAL
    {
        //public bool Add(TaoTianCutDataRequest model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("insert into whs_taotian_upload(");
        //    strSql.Append("user_name, ip_address, pass_word, port_no, channel_no, waybill_code, stock_code, start_time, end_time, picking_list_no)");
        //    strSql.Append(" values (");
        //    strSql.Append("@userName, @ipAddress, @passWord, @portNo, @channelNo, @waybillCode, @stockCode, @startTime, @endTime, @pickingListNo)");
        //    SqliteParameter[] parameters = {
        //                  new SqliteParameter("@userName", SqliteType.Text, 50),
        //    new SqliteParameter("@ipAddress", SqliteType.Text, 50),
        //    new SqliteParameter("@passWord", SqliteType.Text, 25),
        //    new SqliteParameter("@portNo", SqliteType.Text, 50),
        //    new SqliteParameter("@channelNo", SqliteType.Text, 50),
        //    new SqliteParameter("@waybillCode", SqliteType.Text, 50),
        //    new SqliteParameter("@stockCode", SqliteType.Text, 50),
        //    new SqliteParameter("@startTime", SqliteType.Integer),
        //    new SqliteParameter("@endTime", SqliteType.Integer),
        //    new SqliteParameter("@pickingListNo", SqliteType.Text, 50)};
        //    parameters[0].Value = model.userName;
        //    parameters[1].Value = model.ipAddress;
        //    parameters[2].Value = model.passWord;
        //    parameters[3].Value = model.portNo;
        //    parameters[4].Value = model.channelNo;
        //    parameters[5].Value = model.waybillCode;
        //    parameters[6].Value = model.stockCode;
        //    parameters[7].Value = model.startTime;
        //    parameters[8].Value = model.endTime;
        //    parameters[9].Value = model.pickingListNo;
        //    int rows = SQLiteHelper.ExecuteSql(strSql.ToString(), parameters);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from whs_taotian_upload ");
            strSql.Append(" where id=@id ");
            SqliteParameter[] parameters = {
                     new SqliteParameter("@id", SqliteType.Integer,8)          };
            parameters[0].Value = ID;
            int rows = SQLiteHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM whs_taotian_upload ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SQLiteHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetLogList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM whs_taotian_upload_log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by start_time asc ");
            return SQLiteHelper.Query(strSql.ToString());
        }
        /// <summary>
        /// 生成sql
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public String GetGenSQL(TaoTianCutDataRequest model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into whs_taotian_upload(");
            strSql.Append("user_name, ip_address, pass_word, port_no, channel_no, waybill_code, stock_code, start_time, end_time, picking_list_no)");
            strSql.Append(" values (");
            strSql.Append("'" + model.userName + "', '" + model.ipAddress + "', '" + model.passWord + "', '" + model.portNo + "', '" + model.channelNo + "', '" + model.waybillCode + "', '" + model.stockCode + "', " + model.startTime + ", " + model.endTime + ", '" + model.pickingListNo + "')");
            string query = strSql.ToString();
            return query;
        }

        /// <summary>
        /// 生成sql
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public String GetGenCutSQL(TaoTianCutDataRequest model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into whs_taotian_upload(");
            strSql.Append("user_name, ip_address, pass_word, port_no, channel_no, waybill_code, stock_code, start_time, end_time, picking_list_no)");
            strSql.Append(" values (");
            strSql.Append("'" + model.userName + "', '" + model.ipAddress + "', '" + model.passWord + "', '" + model.portNo + "', '" + model.channelNo + "', '" + model.waybillCode + "', '" + model.stockCode + "', " + model.startTime + ", " + model.endTime + ", '" + model.pickingListNo + "')");
            // 将 StringBuilder 转换为字符串
            string query = strSql.ToString();
            return query;
        }

        public bool ExcuteGenSQL(TaoTianCutDataRequest model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into whs_taotian_upload(");
            strSql.Append("user_name, ip_address, pass_word, port_no, channel_no, waybill_code, stock_code, start_time,end_time, picking_list_no)");
            strSql.Append(" values (");
            strSql.Append("'" + model.userName + "', '" + model.ipAddress + "', '" + model.passWord + "', '" + model.portNo + "', '" + model.channelNo + "', '" + model.waybillCode + "', '" + model.stockCode + "', " + model.startTime + "," + model.endTime + ", '" + model.pickingListNo + "')");
            // 将 StringBuilder 转换为字符串
            string query = strSql.ToString();
            int rows = SQLiteHelper.ExecuteSql(query);
            if (rows > 0)
            { return true; }
            else
            { return false; }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool Update(long ID, long endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE whs_taotian_upload_log SET ");
            strSql.Append("end_time = " + endTime + " ");
            strSql.Append(" WHERE id = " + ID + "");
            int rows = SQLiteHelper.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExcuteGenLogSQL(TaoTianCutDataRequest model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into whs_taotian_upload_log(");
            strSql.Append("user_name, ip_address, pass_word, port_no, channel_no, waybill_code, stock_code, start_time, picking_list_no)");
            strSql.Append(" values (");
            strSql.Append("'" + model.userName + "', '" + model.ipAddress + "', '" + model.passWord + "', '" + model.portNo + "', '" + model.channelNo + "', '" + model.waybillCode + "', '" + model.stockCode + "', " + model.startTime + ", '" + model.pickingListNo + "')");
            // 将 StringBuilder 转换为字符串
            string query = strSql.ToString();
            int rows = SQLiteHelper.ExecuteSql(query);
            if (rows > 0)
            { return true; }
            else
            { return false; }
        }

        /// <summary>
        /// 批量执行
        /// </summary>
        /// <param name="SQLStringList"></param>
        /// <returns></returns>
        public bool BathAdd(ArrayList SQLStringList)
        {
            try
            {
                SQLiteHelper.ExecuteSqlTran(SQLStringList);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.LoggerError(typeof(TaoTianHikDAL), $"事务执行失败: {ex.Message}");
                return false;
            }
        }

    }

}
