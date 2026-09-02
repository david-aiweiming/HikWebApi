using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikWebApi
{
    public static class HikErrorMsg
    {
        public static string HikLoginErrorMessage(uint lastError)
        {
            switch (lastError)
            {
                case 1:
                    return "用户名或者密码错误";
                case 4:
                    return "通道错误";
              
                case 153:
                    return "用户被锁定";
                case 152:
                    return "用户不存在";
                case 23:
                    return "设备不支持";
                case 2:
                    return "权限不足";
                case 5:
                    return "设备总的连接数超过最大";
                case 7:
                    return "连接设备失败，设备不在线或网络原因引起的连接超时等。";

                default:
                    return "error code= " + lastError; 

            }
        }
    }
}
