using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace callExpressRequest
{
    [TestFixture]
    public  class TestCallExpressService
    {
        [Test]
        public void TEST()
        {
            String partnerID = "SLKJ2019";//此处替换为您在丰桥平台获取的顾客编码          
            String checkword = "FBIqMkZjzxbsZgo7jTpeq7PD8CVzLT4Q";//此处替换为您在丰桥平台获取的校验码            

            String reqURL = "https://sfapi-sbox.sf-express.com/std/service";
            //String reqURL = "https://sfapi.sf-express.com/std/service"; //生产环境

            //将callExpressRequest文件件放于{program_}/Debug/文件夹下

             String serviceCode = "EXP_RECE_CREATE_ORDER";
             String path = @"E:\WorkFile\NUnitTestProject1\callExpressRequest\01.order.json";//下订单
             
           // String serviceCode = "EXP_RECE_SEARCH_ORDER_RESP";
           // String path = "./callExpressRequest/02.order.query.json";//订单结果查询

           // String serviceCode = "EXP_RECE_UPDATE_ORDER";
          //  String path = "./callExpressRequest/03.order.confirm.json";//订单确认取消

          //   String serviceCode = "EXP_RECE_FILTER_ORDER_BSP";
          //   String path = "./callExpressRequest/04.order.filter.json";//订单筛选	

            //String serviceCode = "EXP_RECE_SEARCH_ROUTES";
            //String path = "./callExpressRequest/05.route_query_by_MailNo.json";//路由查询-通过运单号
            //String path = "./callExpressRequest/05.route_query_by_OrderNo.json";//路由查询-通过订单号

            //String serviceCode = "EXP_RECE_GET_SUB_MAILNO";
           // String path = "./callExpressRequest/07.sub.mailno.json";//子单号申请

           //  String serviceCode = "EXP_RECE_QUERY_SFWAYBILL";
           // String path = "./callExpressRequest/09.waybills_fee.json";//清单运费查询            

            String msgJson = "{	\"cargoDetails\":[{		\"amount\":308.0,		\"count\":1.0,		\"name\":\"君宝牌地毯\",		\"unit\":\"个\",		\"volume\":0.0,		\"weight\":0.1	}],	\"contactInfoList\":[{		\"address\":\"十堰市丹江口市公园路155号\",		\"city\":\"十堰市\",		\"company\":\"清雅轩保健品专营店\",		\"contact\":\"张三丰\",		\"contactType\":1,		\"county\":\"武当山风景区\",		\"mobile\":\"17006805888\",		\"province\":\"湖北省\"	},{		\"address\":\"湖北省襄阳市襄城区环城东路122号\",		\"city\":\"襄阳市\",		\"contact\":\"郭襄阳\",		\"county\":\"襄城区\",		\"contactType\":2,		\"mobile\":\"18963828829\",		\"province\":\"湖北省\"	}],	\"customsInfo\":{},	\"expressTypeId\":1,	\"extraInfoList\":[],	\"isOneselfPickup\":0,	\"language\":\"zh-CN\",	\"monthlyCard\":\"7551234567\",	\"orderId\":\"QIAO-58693569365\",	\"parcelQty\":1,	\"payMethod\":1,	\"totalWeight\":6}";
            String msgData = JsonCompress(msgJson);

            String timestamp = GetTimeStamp(); //获取时间戳       

            String requestID = System.Guid.NewGuid().ToString(); //获取uuid

            String msgDigest = MD5ToBase64String(UrlEncode(msgData + timestamp + checkword));

            Console.WriteLine("partnerID: " + partnerID);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("checkword: " + checkword);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("timestamp: " + timestamp);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("requestID: " + requestID);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("msgDigest: " + msgDigest);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("请求报文: " + (msgData + timestamp + checkword));
            Console.WriteLine("--------------------------------------");
            

            String respJson = callSfExpressServiceByCSIM(reqURL, partnerID, requestID, serviceCode, timestamp, msgDigest, msgData);

            if (respJson != null)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("返回报文: " + respJson);
                Console.WriteLine("--------------------------------------");
                Console.ReadKey(true);
            }

        }

        private static string JsonCompress(string msgData)
        {
            StringBuilder sb = new StringBuilder();
            using (StringReader reader = new StringReader(msgData))
            {
                int ch = -1;
                int lastch = -1;
                bool isQuoteStart = false;
                while ((ch = reader.Read()) > -1)
                {
                    if ((char)lastch != '\\' && (char)ch == '\"')
                    {
                        if (!isQuoteStart)
                        {
                            isQuoteStart = true;
                        }
                        else
                        {
                            isQuoteStart = false;
                        }
                    }
                    if (!Char.IsWhiteSpace((char)ch) || isQuoteStart)
                    {
                        sb.Append((char)ch);
                    }
                    lastch = ch;
                }
            }
            return sb.ToString();
        }

        private static string callSfExpressServiceByCSIM(string reqURL, string partnerID, string requestID, string serviceCode, string timestamp, string msgDigest, string msgData)
        {
            String result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(reqURL);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            
            Dictionary<string, string> content = new Dictionary<string, string>();
            content["partnerID"] = partnerID;
            content["requestID"] = requestID;
            content["serviceCode"] = serviceCode;
            content["timestamp"] = timestamp;
            content["msgDigest"] = msgDigest;
            content["msgData"] = msgData;

            if (!(content == null || content.Count ==0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in content.Keys)
                {
                    if(i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, content[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, content[key]);
                    }
                    i++;
                }

                byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());                
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                
            }
            
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;            
        }

        private static string UrlEncode(string str)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in str)
            {
                if (System.Web.HttpUtility.UrlEncode(c.ToString()).Length > 1)
                {
                    builder.Append(System.Web.HttpUtility.UrlEncode(c.ToString()).ToUpper());
                }
                else
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }

        private static string MD5ToBase64String(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] MD5 = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));//MD5(注意UTF8编码)
            string result = Convert.ToBase64String(MD5);//Base64
            return result;
        }

        private static string Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.UTF8);

            StringBuilder builder = new StringBuilder();
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                builder.Append(line);
            }
            return builder.ToString();
        }

        private static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }        
    }
}