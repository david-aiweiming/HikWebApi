using HikWebApi.Model;
using HikWebApi.TaoTianDAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using static HikWebApi.CHCNetSDK;
using static HikWebApi.Model.HikModel;
using static HikWebApi.Model.TaoTianHikModel;

namespace HikWebApi.Controllers
{

    /// <summary>
    /// Hik接口
    /// </summary>
    [Route("api")]
    [ApiController]
    public class HikController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private bool m_bInitSDK = false;
        private uint iLastErr = 0;
        private int m_lUserID = -1;
        public NET_DVR_DEVICEINFO_V30 DeviceInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public HikController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// 设置OSD信息
        /// </summary>
        /// <param name="hikOsdRequest"></param>
        /// <returns></returns>
        [HttpPost("sethikosd")]
        public HikSetOsdResponse SetHikOsd([FromBody] HikSetOsdRequest hikOsdRequest)
        {
            try
            {
                LogHelper.LoggerError(typeof(HikController), "设置水印信息：" + JsonConvert.SerializeObject(hikOsdRequest));
                if (string.IsNullOrEmpty(hikOsdRequest.osdMessage))
                {
                    LogHelper.LoggerError(typeof(HikController), "入参osdMessage为空");
                    return new HikSetOsdResponse
                    {
                        code = -1,
                        message = "入参osdMessage为空"
                    };
                }
                m_bInitSDK = NET_DVR_Init();
                if (!m_bInitSDK)
                {
                    iLastErr = NET_DVR_GetLastError();
                    LogHelper.LoggerError(typeof(HikController), "NET_DVR_Init failed, error code= " + iLastErr);
                    return new HikSetOsdResponse
                    {
                        code = -1,
                        message = "NET_DVR_Init failed, error code= " + iLastErr
                    };
                }
                if (m_lUserID < 0)
                {
                    string DVRIPAddress = hikOsdRequest.ipAddress; //设备IP地址或者域名
                    Int16 DVRPortNumber = Int16.Parse(hikOsdRequest.portNo);//设备服务端口号
                    string DVRUserName = hikOsdRequest.userName;//设备登录用户名
                    string DVRPassword = hikOsdRequest.passWord;//设备登录密码
                    //登录设备 Login the device
                    m_lUserID = NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                    if (m_lUserID < 0)
                    {
                        iLastErr = NET_DVR_GetLastError();
                        LogHelper.LoggerError(typeof(HikController), "NET_DVR_Login_V30 failed, " + HikErrorMsg.HikLoginErrorMessage(iLastErr));
                        return new HikSetOsdResponse
                        {
                            code = -1,
                            message = "NET_DVR_Login_V30 failed, " + HikErrorMsg.HikLoginErrorMessage(iLastErr)
                        };
                    };
                }

                //GET struShowStrCfg
                NET_DVR_SHOWSTRING_V30 m_struShowStrCfg = new NET_DVR_SHOWSTRING_V30();//初始化叠加字符结构体
                UInt32 dwReturn = 0;
                Int32 nSize = Marshal.SizeOf(m_struShowStrCfg);
                IntPtr ptrShowStrCfg = Marshal.AllocHGlobal(nSize);
                Marshal.StructureToPtr(m_struShowStrCfg, ptrShowStrCfg, false);
                if (!CHCNetSDK.NET_DVR_GetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_GET_SHOWSTRING_V30, Int32.Parse(hikOsdRequest.channelNo), ptrShowStrCfg, (UInt32)nSize, ref dwReturn))
                {
                    iLastErr = NET_DVR_GetLastError();
                    LogHelper.LoggerError(typeof(HikController), "NET_DVR_GET_SHOWSTRING_V30 failed, error code= " + iLastErr);
                    //释放登入信息
                    LoginOutAndCleanup(m_lUserID);
                    return new HikSetOsdResponse
                    {
                        code = -1,
                        message = "NET_DVR_GET_SHOWSTRING_V30 failed, error code= " + iLastErr
                    };
                }
                m_struShowStrCfg = (CHCNetSDK.NET_DVR_SHOWSTRING_V30)Marshal.PtrToStructure(ptrShowStrCfg, typeof(CHCNetSDK.NET_DVR_SHOWSTRING_V30));
                Marshal.FreeHGlobal(ptrShowStrCfg);
                //SET struShowStrCfg
                string osdText = string.Empty;
                if (string.IsNullOrEmpty(hikOsdRequest.waybillCode))
                {
                    osdText = hikOsdRequest.osdMessage;
                }
                else
                {
                    osdText = hikOsdRequest.osdMessage + "  " + hikOsdRequest.waybillCode;
                }
                m_struShowStrCfg.struStringInfo[0].wShowString = 1;//1为显示，0为不显示
                m_struShowStrCfg.struStringInfo[0].sString = osdText;//叠加的字符串
                m_struShowStrCfg.struStringInfo[0].wStringSize = (ushort)(osdText.Length);//字符串大小
                m_struShowStrCfg.struStringInfo[0].wShowStringTopLeftX = 0;//坐标
                m_struShowStrCfg.struStringInfo[0].wShowStringTopLeftY = 0;//坐标
                nSize = Marshal.SizeOf(m_struShowStrCfg);
                ptrShowStrCfg = Marshal.AllocHGlobal(nSize);
                Marshal.StructureToPtr(m_struShowStrCfg, ptrShowStrCfg, false);
                if (!CHCNetSDK.NET_DVR_SetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_SET_SHOWSTRING_V30, Int32.Parse(hikOsdRequest.channelNo), ptrShowStrCfg, (UInt32)nSize))
                {
                    iLastErr = NET_DVR_GetLastError();
                    LogHelper.LoggerError(typeof(HikController), "NET_DVR_GET_SHOWSTRING_V30 failed, error code= " + iLastErr);
                    LoginOutAndCleanup(m_lUserID);
                    return new HikSetOsdResponse
                    {
                        code = -1,
                        message = "NET_DVR_GET_SHOWSTRING_V30 failed, error code= " + iLastErr
                    };
                }
                Marshal.FreeHGlobal(ptrShowStrCfg);
                //释放登入信息
                if (!NET_DVR_Logout(m_lUserID))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    LogHelper.LoggerError(typeof(HikController), "NET_DVR_Logout failed, error code= " + iLastErr);
                    return new HikSetOsdResponse
                    {
                        code = -1,
                        message = "NET_DVR_Logout failed, error code= " + iLastErr
                    };
                }
                //释放SDK信息
                bool m_bClearSDK = NET_DVR_Cleanup();
                if (!m_bClearSDK)
                {

                    iLastErr = NET_DVR_GetLastError();
                    LogHelper.LoggerError(typeof(HikController), "NET_DVR_Clear failed, error code= " + iLastErr);
                    return new HikSetOsdResponse
                    {
                        code = -1,
                        message = "NET_DVR_Clear failed, error code= " + iLastErr
                    };
                }

                long dateTimeTicks = GetChinaTicks(DateTime.Now);
                if (hikOsdRequest.orderTag == "TT")
                {
                    if (!string.IsNullOrEmpty(hikOsdRequest.waybillCode))
                    {
                        TaoTianCutDataRequest taoTianCutDataRequest = new TaoTianCutDataRequest();
                        taoTianCutDataRequest.waybillCode = hikOsdRequest.waybillCode;
                        taoTianCutDataRequest.pickingListNo = hikOsdRequest.osdMessage;
                        taoTianCutDataRequest.stockCode = hikOsdRequest.stockCode;
                        taoTianCutDataRequest.startTime = dateTimeTicks;
                        taoTianCutDataRequest.ipAddress = hikOsdRequest.ipAddress;
                        taoTianCutDataRequest.portNo = hikOsdRequest.portNo;
                        taoTianCutDataRequest.passWord = hikOsdRequest.passWord;
                        taoTianCutDataRequest.userName = hikOsdRequest.userName;
                        taoTianCutDataRequest.channelNo = hikOsdRequest.channelNo;
                        TaoTianHikDAL taoTianHikDAL = new TaoTianHikDAL();
                        if (!taoTianHikDAL.ExcuteGenLogSQL(taoTianCutDataRequest))
                        {
                            return new HikSetOsdResponse
                            {
                                code = -1,
                                message = "捕捉错误：插入到sqllite失败"
                            };
                        }
                    }
                }

                return new HikSetOsdResponse
                {
                    code = 200,
                    data = new HikModel.Data { dateTime = dateTimeTicks },
                    message = string.Empty
                };
            }
            catch (Exception ex)
            {
                LogHelper.LoggerError(typeof(HikController), "捕捉错误：" + ex.StackTrace);
                return new HikSetOsdResponse
                {
                    code = -1,
                    message = "捕捉错误：" + ex.Message
                };
            }
        }

        /// <summary>
        /// 清除OSD信息
        /// </summary>
        /// <param name="hikOsdRequest"></param>
        /// <returns></returns>
        [HttpPost("clearhikosd")]
        public HikSetOsdResponse ClearHikOsd([FromBody] HikSetOsdRequest hikOsdRequest)
        {
            try
            {
                LogHelper.LoggerError(typeof(HikController), "擦除水印信息：" + JsonConvert.SerializeObject(hikOsdRequest));
                m_bInitSDK = NET_DVR_Init();
                if (!m_bInitSDK)
                {
                    iLastErr = NET_DVR_GetLastError();
                    LogHelper.LoggerError(typeof(HikController), "NET_DVR_Init failed, error code= " + iLastErr);
                    return new HikSetOsdResponse
                    {
                        code = -1,
                        message = "NET_DVR_Init failed, error code= " + iLastErr
                    };
                }
                if (m_lUserID < 0)
                {
                    string DVRIPAddress = hikOsdRequest.ipAddress; //设备IP地址或者域名
                    Int16 DVRPortNumber = Int16.Parse(hikOsdRequest.portNo);//设备服务端口号
                    string DVRUserName = hikOsdRequest.userName;//设备登录用户名
                    string DVRPassword = hikOsdRequest.passWord;//设备登录密码
                    //登录设备 Login the device
                    m_lUserID = NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                    if (m_lUserID < 0)
                    {
                        iLastErr = NET_DVR_GetLastError();
                        LogHelper.LoggerError(typeof(HikController), "NET_DVR_Login_V30 failed, " + HikErrorMsg.HikLoginErrorMessage(iLastErr));
                        return new HikSetOsdResponse
                        {
                            code = -1,
                            message = "NET_DVR_Login_V30 failed, " + HikErrorMsg.HikLoginErrorMessage(iLastErr)
                        };
                    };
                }
                //GET struShowStrCfg
                NET_DVR_SHOWSTRING_V30 m_struShowStrCfg = new NET_DVR_SHOWSTRING_V30();//初始化叠加字符结构体
                UInt32 dwReturn = 0;
                Int32 nSize = Marshal.SizeOf(m_struShowStrCfg);
                IntPtr ptrShowStrCfg = Marshal.AllocHGlobal(nSize);
                Marshal.StructureToPtr(m_struShowStrCfg, ptrShowStrCfg, false);
                if (!CHCNetSDK.NET_DVR_GetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_GET_SHOWSTRING_V30, Int32.Parse(hikOsdRequest.channelNo), ptrShowStrCfg, (UInt32)nSize, ref dwReturn))
                {
                    iLastErr = NET_DVR_GetLastError();
                    LogHelper.LoggerError(typeof(HikController), "NET_DVR_GET_SHOWSTRING_V30 failed, error code= " + iLastErr);
                    LoginOutAndCleanup(m_lUserID);
                    return new HikSetOsdResponse
                    {
                        code = -1,
                        message = "NET_DVR_GET_SHOWSTRING_V30 failed, error code= " + iLastErr
                    };
                }
                m_struShowStrCfg = (CHCNetSDK.NET_DVR_SHOWSTRING_V30)Marshal.PtrToStructure(ptrShowStrCfg, typeof(CHCNetSDK.NET_DVR_SHOWSTRING_V30));
                Marshal.FreeHGlobal(ptrShowStrCfg);
                //SET struShowStrCfg
                //string osd = string.Empty;
                m_struShowStrCfg.struStringInfo[0].wShowString = 0;//1为显示，0为不显示
                m_struShowStrCfg.struStringInfo[0].sString = string.Empty;//叠加的字符串
                m_struShowStrCfg.struStringInfo[0].wStringSize = 0;//字符串大小
                m_struShowStrCfg.struStringInfo[0].wShowStringTopLeftX = 0;//坐标
                m_struShowStrCfg.struStringInfo[0].wShowStringTopLeftY = 0;//坐标
                nSize = Marshal.SizeOf(m_struShowStrCfg);
                ptrShowStrCfg = Marshal.AllocHGlobal(nSize);
                Marshal.StructureToPtr(m_struShowStrCfg, ptrShowStrCfg, false);
                if (!CHCNetSDK.NET_DVR_SetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_SET_SHOWSTRING_V30, Int32.Parse(hikOsdRequest.channelNo), ptrShowStrCfg, (UInt32)nSize))
                {
                    iLastErr = NET_DVR_GetLastError();
                    LogHelper.LoggerError(typeof(HikController), "NET_DVR_GET_SHOWSTRING_V30 failed, error code= " + iLastErr);
                    //释放登入信息
                    LoginOutAndCleanup(m_lUserID);
                    return new HikSetOsdResponse
                    {
                        code = -1,
                        message = "NET_DVR_GET_SHOWSTRING_V30 failed, error code= " + iLastErr
                    };
                }
                Marshal.FreeHGlobal(ptrShowStrCfg);
                //释放登入信息
                if (!NET_DVR_Logout(m_lUserID))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    LogHelper.LoggerError(typeof(HikController), "NET_DVR_Logout failed, error code= " + iLastErr);
                    return new HikSetOsdResponse
                    {
                        code = -1,
                        message = "NET_DVR_Logout failed, error code= " + iLastErr
                    };
                }
                //释放SDK信息
                bool m_bClearSDK = NET_DVR_Cleanup();
                if (!m_bClearSDK)
                {
                    iLastErr = NET_DVR_GetLastError();
                    LogHelper.LoggerError(typeof(HikController), "NET_DVR_Clear failed, error code= " + iLastErr);
                    return new HikSetOsdResponse
                    {
                        code = -1,
                        message = "NET_DVR_Clear failed, error code= " + iLastErr
                    };
                }
                string isSplitVideo = _configuration["AppSettings:IsSplitVideo"] ?? "0";
                //如果是淘天的订单，就插入表
                long dateTimeTicks = GetChinaTicks(DateTime.Now);
                if (hikOsdRequest.orderTag == "TT" || isSplitVideo == "1")
                {
                    if (!string.IsNullOrEmpty(hikOsdRequest.osdMessage))
                    {
                        TaoTianHikDAL taoTianHikDAL = new TaoTianHikDAL();
                        string strWhere = "picking_list_no = '" + hikOsdRequest.osdMessage + "'";
                        DataTable dt = taoTianHikDAL.GetLogList(strWhere);
                        List<TaoTianCutDataRequest> taoTianCutDataList = DataTableToList(dt);
                        // 遍历数据行
                        // 定义一个变量用于更新结束时间
                        int lastUpdatedIndex = 0;
                        // 遍历数据行
                        for (int i = 1; i < taoTianCutDataList.Count; i++)
                        {
                            // 计算时间差
                            //long timeDiff = taoTianCutDataList[i].startTime - taoTianCutDataList[i - 1].startTime;
                            // 如果时间差大于10秒
                            // 将毫秒时间戳转换为DateTime
                        
                            DateTime dateTime1 = ConvertToBeijingTime(taoTianCutDataList[i - 1].startTime);
                            DateTime dateTime2 = ConvertToBeijingTime(taoTianCutDataList[i].startTime);

                            // 计算时间间隔
                            TimeSpan timeDifference = dateTime2 - dateTime1;
                            if (timeDifference.TotalSeconds > 20)
                            {
                                // 更新结束时间
                                for (int j = lastUpdatedIndex; j < i; j++)
                                {
                                    taoTianHikDAL.Update(taoTianCutDataList[j].id, taoTianCutDataList[i].startTime);
                                    taoTianCutDataList[j].endTime = taoTianCutDataList[i].startTime;
                                }

                                // 更新 lastUpdatedIndex
                                lastUpdatedIndex = i;
                            }
                        }
                        // 对于最后一行或者循环到后面没有数据了的情况，使用变量更新结束时间
                        //int lastTimeStamp = taoTianCutDataList.Count > 0 ? taoTianCutDataList[taoTianCutDataList.Count - 1].startTime : 0;
                        for (int j = lastUpdatedIndex; j < taoTianCutDataList.Count; j++)
                        {   
                            taoTianHikDAL.Update(taoTianCutDataList[j].id, dateTimeTicks);
                            taoTianCutDataList[j].endTime = dateTimeTicks;
                        }

                        // 分组并获取每组最小的 startTime 和拼接的 waybillCode
                        var result = taoTianCutDataList.GroupBy(
                            d => new { d.endTime, d.userName, d.ipAddress, d.passWord, d.portNo, d.channelNo,d.pickingListNo,d.stockCode },
                            (key, group) => new
                            {
                                key.endTime,
                                key.userName,
                                key.ipAddress,
                                key.passWord,
                                key.portNo,
                                key.channelNo,
                                key.pickingListNo,
                                key.stockCode,
                                minStartTime = group.Min(d => d.startTime),
                                waybillCodes = string.Join(",", group.Select(d => d.waybillCode))
                            }
                        );

                        ArrayList sqlList = new ArrayList();
                        foreach (var item in result)
                        {
                            TaoTianCutDataRequest dataRequest = new TaoTianCutDataRequest
                            {
                                userName = item.userName,
                                ipAddress = item.ipAddress,
                                passWord = item.passWord,
                                portNo = item.portNo,
                                channelNo = item.channelNo,
                                endTime = item.endTime,
                                startTime = item.minStartTime,
                                waybillCode = item.waybillCodes,
                                pickingListNo = item.pickingListNo,
                                stockCode = item.stockCode,
                                orderTag=item.orderTag
                            };
                            string sql = taoTianHikDAL.GetGenCutSQL(dataRequest);
                            sqlList.Add(sql);
                        }
                        if (sqlList != null && sqlList.Count > 0)
                        {
                            if (!taoTianHikDAL.BathAdd(sqlList))
                            {
                                return new HikSetOsdResponse
                                {
                                    code = -1,
                                    message = "捕捉错误：插入到sqllite失败"
                                };
                            }
                        }
                    }
                }

                return new HikSetOsdResponse
                {
                    code = 200,
                    data = new HikModel.Data { dateTime = dateTimeTicks },
                    message = string.Empty
                };
            }
            catch (Exception ex)
            {
                LogHelper.LoggerError(typeof(HikController), "捕捉错误：" + ex.Message);
                return new HikSetOsdResponse
                {
                    code = -1,
                    message = "捕捉错误：" + ex.Message
                };
            }
        }
        public static List<TaoTianCutDataRequest> DataTableToList(DataTable dt)
        {
            List<TaoTianCutDataRequest> taoTianCutDataList = new List<TaoTianCutDataRequest>();
            foreach (DataRow row in dt.Rows)
            {
                TaoTianCutDataRequest taoTianCutDataRequest = new TaoTianCutDataRequest();
                taoTianCutDataRequest.id = Convert.ToInt64(row["id"]);
                taoTianCutDataRequest.userName = row["user_name"].ToString();
                taoTianCutDataRequest.ipAddress = row["ip_address"].ToString();
                taoTianCutDataRequest.passWord = row["pass_word"].ToString();
                taoTianCutDataRequest.portNo = row["port_no"].ToString();
                taoTianCutDataRequest.channelNo = row["channel_no"].ToString();
                taoTianCutDataRequest.waybillCode = row["waybill_code"].ToString();
                taoTianCutDataRequest.stockCode = row["stock_code"].ToString();
                taoTianCutDataRequest.startTime = Convert.ToInt64(row["start_time"]);
                taoTianCutDataRequest.pickingListNo = row["picking_list_no"].ToString();
                taoTianCutDataList.Add(taoTianCutDataRequest);
            }

            return taoTianCutDataList;
        }
        public static DateTime ConvertToBeijingTime(long timestamp)
        {
            // 将时间戳转换为DateTime对象
            DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;

            // 将DateTime对象从UTC时间转换为北京时间
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime beijingTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZoneInfo);

            return beijingTime;
        }
        private void LoginOutAndCleanup(int m_lUserID)
        {
            if (!NET_DVR_Logout(m_lUserID))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                LogHelper.LoggerError(typeof(HikController), "NET_DVR_Logout failed, error code= " + iLastErr);

            }
            bool m_bClearSDK = NET_DVR_Cleanup();
            if (!m_bClearSDK)
            {
                iLastErr = NET_DVR_GetLastError();
                LogHelper.LoggerError(typeof(HikController), "NET_DVR_Clear failed, error code= " + iLastErr);
            }
        }


        public static long GetChinaTicks(DateTime dateTime)
        {
            //北京时间相差8小时
            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 8, 0, 0, 0), TimeZoneInfo.Local);
            long tick = (dateTime.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位   
            return tick;
        }


        /// <summary>
        /// 结束淘天结束数据
        /// </summary>
        /// <param name="taoTianCutDataRequestList"></param>
        /// <returns></returns>
        [HttpPost("endtaotiancut")]
        public TaoTianCutDataResponse EndTaoTianCut([FromBody] List<TaoTianCutDataRequest> taoTianCutDataRequestList)
        {
            try
            {
                LogHelper.LoggerInfo(typeof(TaoTianCutDataResponse), "接受到截取信息：" + JsonConvert.SerializeObject(taoTianCutDataRequestList));
                //把请求数据插入到sqllite
                TaoTianHikDAL taoTianHikDAL = new TaoTianHikDAL();
                ArrayList sqlList = new ArrayList();
                foreach (var item in taoTianCutDataRequestList)
                {
                    if (item.orderTag == "TT")
                    {
                        string sql = taoTianHikDAL.GetGenSQL(item);
                        sqlList.Add(sql);
                    }
                }
                if (sqlList != null && sqlList.Count > 0)
                {
                    if (!taoTianHikDAL.BathAdd(sqlList))
                    {
                        return new TaoTianCutDataResponse
                        {
                            code = -1,
                            message = "捕捉错误：插入到sqllite失败"
                        };
                    }
                }
                return new TaoTianCutDataResponse
                {
                    code = 200,
                    message = string.Empty
                };
            }
            catch (Exception ex)
            {
                LogHelper.LoggerError(typeof(TaoTianCutDataResponse), "捕捉错误：" + ex.Message);
                return new TaoTianCutDataResponse
                {
                    code = -1,
                    message = "捕捉错误：" + ex.Message
                };
            }
        }
    }


}
