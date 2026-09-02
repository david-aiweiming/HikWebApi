using HWMSWEBAPI.Common;
using HWMSWEBAPI.DBUtility;
using HWMSWEBAPI.Express;
using HWMSWEBAPI.IDAL;
using HWMSWEBAPI.Model;
using HWMSWEBAPI.MySQLDAL;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject1
{
    [TestFixture]
    public class TestClass3
    {
        [Test]
        //public void TestMethod()
        //{

        //    //加载appSetting
        //    var builder = new ConfigurationBuilder()
        //         .SetBasePath(Directory.GetCurrentDirectory())
        //         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //         .AddEnvironmentVariables();
        //    IConfiguration configuration = builder.Build();
        //    new AppSettingModel().Initial(configuration);
        //   // TODO: Add your test code here
        //   var answer = "{    \"action\":\"waybill_print_task\",    \"message_id\":\"464313216465464\",    \"request_params\":{        \"platform_code\":\"ZTO\",        \"owner_code\":\"GAOLANG\",        \"stock_code\":\"HD-JIA-02\",        \"cp_code\":\"YTO\",        \"template_name\":\"STO\",        \"contents\":[            {                \"receive_info\":{                    \"receive_province\":\"上海\",                    \"receive_city\":\"上海市\",                    \"receive_area\":\"松江区\",                    \"receive_town\":\"\",                    \"receive_address_detail\":\"古楼公路1458弄8888号楼999\",                    \"receive_phone\":\"07933322336\",                    \"receive_name\":\"林语语\"                },                \"send_info\":{                    \"send_province\":\"浙江省\",                    \"send_city\":\"金华市\",                    \"send_area\":\"金东区区\",                    \"send_town\":\"\",                    \"send_address_detail\":\"浙江省金华市金东区菜鸟网络金义园区一期7号库3分区12号门\",                    \"send_name\":\"杭州高浪科技有限公司\",                    \"send_phone\":\"057125855252\"                },                \"trade_order_info\":{                    \"order_no\":\"ON2582255555555\",                    \"waybill_no\":\"689898989\",                    \"total_fees\":300,                    \"insured_fees\":300,                    \"freight\":5,                    \"pay_mothod\":\"寄付月结\",                    \"total_weight\":12,                    \"monthly_account\":\"57976589\",                    \"short_address\":\"380\",                    \"package_center_code\":\"F45-00-32\",                    \"package_center_name\":\"【区域】上海转运中心\",                    \"one_section_code\":\"600\",                    \"two_section_code\":\"Q115-00  67\",                    \"three_section_code\":\"\",                    \"order_items\":[                        {                            \"item_name\":\"DERMALOGICA/德美乐嘉手打酵素洁颜粉\",                            \"item_code\":\"333\",                            \"item_qty\":\"1\"                        }                    ]                },                \"customer_area\":{                    \"wave_code\":\"BCDANG201236258411256\",                    \"shipment_code\":\"RELANG202010270007\",                    \"package_code\":\"PN2020102700008\",                    \"total_sku_qty\":\"2\",                    \"basket_no\":\"8\"                }            }        ]    }}";
        //    //Assert.That(answer, Is.EqualTo(42), "Some useful error message");

        //    var request = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(answer);
        //    JObject jObject = request.request_params as JObject;
        //    PrintWaybillCodeModel requestParams11 = new PrintWaybillCodeModel();
        //    PrintWaybillCodeModel requestParams = Newtonsoft.Json.JsonConvert.DeserializeObject<PrintWaybillCodeModel>(request.request_params.ToString());
        //    requestParams11 = requestParams;

        //    DataTable dt = new DataTable();
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.AppendFormat("select top 200 * from REL_HDR WHERE LEN(TRACKING_NO) > 0");
        //    DataSet ds = DbHelperMSSQL.Query(strSql.ToString());
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        dt = ds.Tables[0];

        //    }

        //    for (int i = 0; i < 100; i++)
        //    {
        //        var strJson = string.Empty;

        //        PrintWaybillCodeModel requestParams1 = new PrintWaybillCodeModel();
        //        requestParams1 = Newtonsoft.Json.JsonConvert.DeserializeObject<PrintWaybillCodeModel>(request.request_params.ToString());

        //        //requestParams1.contents[0].trade_order_info.waybill_no = "689898989" + "W" + i.ToString();



        //        requestParams1.contents[0].trade_order_info.waybill_no = dt.Rows[i]["TRACKING_NO"].ToString();
        //        requestParams1.contents[0].receive_info.receive_province = dt.Rows[i]["CON_PROVINCE"].ToString();
        //        requestParams1.contents[0].receive_info.receive_city = dt.Rows[i]["CON_CITY"].ToString();
        //        requestParams1.contents[0].receive_info.receive_area = dt.Rows[i]["CON_ZONE"].ToString();
        //        requestParams1.contents[0].receive_info.receive_address_detail = dt.Rows[i]["CON_ADDRESS"].ToString();
        //        requestParams1.contents[0].receive_info.receive_phone = dt.Rows[i]["CON_CONTACT_PHONE"].ToString();
        //        requestParams1.contents[0].receive_info.receive_name = dt.Rows[i]["CON_CONTACT_NAME"].ToString();

        //        //requestParams.contents[1].trade_order_info.waybill_no = requestParams.contents[1].trade_order_info.waybill_no + "W" + i.ToString();
        //        requestParams11.contents.Add(requestParams1.contents[0]);


        //    }
        //    request.request_params = requestParams11;
        //    string oo = Newtonsoft.Json.JsonConvert.SerializeObject(request);

        //    using (var socket = new WebSocketSharp.WebSocket("ws://127.0.0.1:18080/websocket"))
        //    {
        //        socket.Connect();
        //        socket.Send(oo);
        //        socket.OnMessage += (sender, e) =>
        //        {
        //            if (!string.IsNullOrWhiteSpace(e.Data))
        //            {
        //                //CNPrinterPo p = JSON.ToObject<CNPrinterPo>(e.Data);
        //                //action(p);
        //            }

        //        };
        //    }

        //}



        //[Test]
        //public void TestMethod1()
        //{


        //    //加载appSetting
        //    var builder = new ConfigurationBuilder()
        //         .SetBasePath(Directory.GetCurrentDirectory())
        //         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //         .AddEnvironmentVariables();
        //    IConfiguration configuration = builder.Build();
        //    new AppSettingModel().Initial(configuration);
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select * from ba_waybill_log where order_no='4812079649051139955'");
        //    DataSet ds = DbHelperMySQL.Query(strSql.ToString());
        //    DataTable dt = ds.Tables[0];
        //    ISeWaybillResponseDAL seWaybillResponseDAL = DataAccess.CreateSeWaybillResponseDAL();
        //    foreach (DataRow row in dt.Rows)
        //    {

        //        var request = Newtonsoft.Json.JsonConvert.DeserializeObject<waybill_apply_response>(row["log_remark"].ToString());

        //        SeWaybillResponse seWaybillResponse = new SeWaybillResponse();


        //        if (!string.IsNullOrEmpty(row["waybill_code"].ToString()))
        //        {
        //            seWaybillResponse.stock_code = "DT_JYGGWMS0131";
        //            seWaybillResponse.carrier_code = "ZTO";
        //            seWaybillResponse.waybill_code = row["waybill_code"].ToString();
        //            seWaybillResponse.order_no = request.waybill_apply_info[0].order_no;
        //            seWaybillResponse.one_section_code = request.waybill_apply_info[0].one_section_code;
        //            seWaybillResponse.two_section_code = request.waybill_apply_info[0].two_section_code;
        //            seWaybillResponse.three_section_code = request.waybill_apply_info[0].three_section_code;
        //            seWaybillResponse.package_center_code = request.waybill_apply_info[0].package_center_code;
        //            seWaybillResponse.package_center_name = request.waybill_apply_info[0].package_center_name;
        //            seWaybillResponse.short_address = request.waybill_apply_info[0].short_address;
        //            seWaybillResponse.edit_date = DateTime.Now;
        //            SeWaybillResponse seWaybill = seWaybillResponseDAL.GetSeWaybillResponse(seWaybillResponse);
        //            //if (seWaybill == null)
        //            //{
        //            //    seWaybillResponseDAL.Add(seWaybillResponse);
        //            //}
        //            //else
        //            //{
        //            //    seWaybillResponseDAL.Update(seWaybillResponse);
        //            //}
        //        }

        //    }

        //}

        [Test]
        public void TestMethod1()
        {


            //加载appSetting
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables();
            IConfiguration configuration = builder.Build();
            new AppSettingModel().Initial(configuration);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *  from ba_waybill_log where order_no in (select waybill_code from temp_123) AND log_type=1");
            DataSet ds = DbHelperMySQL.Query(strSql.ToString());
            DataTable dt = ds.Tables[0];
            ISeWaybillResponseDAL seWaybillResponseDAL = DataAccess.CreateSeWaybillResponseDAL();
            foreach (DataRow row in dt.Rows)
            {

                var request = Newtonsoft.Json.JsonConvert.DeserializeObject<waybill_apply_request>(row["log_remark"].ToString());

                SeWaybillResponse seWaybillResponse = new SeWaybillResponse();



                PDD pDD = new PDD();
                waybill_apply_response waybill_Apply_ = pDD.GetWayBillCode(request);


            }

        }


        //[Test]
        //public void TestMethod2()
        //{

        //    //string con= DEncryptHelper.Decrypt("m1sESFmA3z5IXi/6dHkGe1Eh/YD0NT6w+2f0W8T1wU0aZ3TLevJfFcstBSyXzQMxORemLpsuoLuLo2EMXo/wIleHOndUu6LfMYbfu6GzkbtRCCcXHyBh5w==");


        //    // string kk = DEncryptHelper.Encrypt("server=192.168.20.170;database=HWMS_DAITA_JHA;uid=sa;password=Danding123456@");
        //    // string con1 = DEncryptHelper.Encrypt("server=192.168.20.170;database=hwms_daita_cfg;uid=sa;password=Danding123456@");
        //    var builder = new ConfigurationBuilder()
        //      .SetBasePath(Directory.GetCurrentDirectory())
        //      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //      .AddEnvironmentVariables();
        //    IConfiguration configuration = builder.Build();
        //    new AppSettingModel().Initial(configuration);
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select * from ba_waybill_log where order_no='4811810457672449147'");
        //    DataSet ds = DbHelperMySQL.Query(strSql.ToString());
        //    DataTable dt = ds.Tables[0];
        //    ISeWaybillResponseDAL seWaybillResponseDAL = DataAccess.CreateSeWaybillResponseDAL();

        //    var waybillApplyRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<waybill_apply_request>(dt.Rows[0]["log_remark"].ToString());
        //    var singleData = GetEMSPrintData("2", "9742480524716", waybillApplyRequest);
        //    var emsPrintData = new EmsWebPrintDatas()
        //    {
        //        sysAccount = "1100041984963",
        //        passWord = "123456",
        //        appKey = "T5675AA9D19919D11",
        //        printKind = "2",
        //        printDatas = new[] { singleData }
        //    };

        //    var EmsResponse = this.UploadEMSData(emsPrintData, "http://os.11183.com.cn:8081/zkweb/bigaccount/getBigAccountDataAction.do");
        //    if (!string.Equals(EmsResponse.result, "1", StringComparison.OrdinalIgnoreCase))
        //    {
        //        //return new waybill_apply_response
        //        //{
        //        //    success = false,
        //        //    message = ""
        //        //};
        //    }
        //}


        public EmsWebResponse UploadEMSData(EmsWebPrintDatas request, string serverUrl)
        {
            var bizData = XmlParser<EmsWebPrintDatas>.ToXml(request);
            bizData = EncodingHelper.Base64Encode(bizData);
            bizData = EncodingHelper.UrlEncode(bizData);
            var method = EncodingHelper.UrlEncode("updatePrintDatas");

            var queryString = string.Format("method={0}&xml={1}", method, bizData);

            var operResult = HttpUtil.Post(serverUrl, queryString);

            var response = new EmsWebResponse();
            if (operResult.IsSuccess)
            {
                var responseString = EncodingHelper.Base64Decode(operResult.SuccessMsg);
                try
                {
                    response = XmlParser<EmsWebResponse>.FromXml(responseString);
                }
                catch (Exception ex)
                {
                    response.result = "0";
                    response.errorDesc = string.Format("{0}{1}{2}", ex.Message, Environment.NewLine, responseString);
                }
            }
            else
            {
                response.result = "0";
                response.errorDesc = operResult.ErrMsg;
            }

            return response;
        }
        private EmsPrintData GetEMSPrintData(string bizKind, string mailNo, waybill_apply_request waybillApplyRequest)
        {
            var result = new EmsPrintData()
            {
                businessType = bizKind,
                scontactor = waybillApplyRequest.trade_order_info_cols.send_name,
                scustMobile = waybillApplyRequest.trade_order_info_cols.send_phone,
                scustTelplus = waybillApplyRequest.trade_order_info_cols.send_phone,
                scustAddr = waybillApplyRequest.shipping_address.address_detail,
                scustProvince = waybillApplyRequest.shipping_address.province,
                scustCity = waybillApplyRequest.shipping_address.city,
                scustCounty = waybillApplyRequest.shipping_address.area,


                tcontactor = waybillApplyRequest.trade_order_info_cols.consignee_name,
                tcustMobile = waybillApplyRequest.trade_order_info_cols.consignee_phone,
                tcustTelplus = waybillApplyRequest.trade_order_info_cols.consignee_phone,
                tcustAddr = waybillApplyRequest.trade_order_info_cols.consignee_address.address_detail,
                tcustProvince = waybillApplyRequest.trade_order_info_cols.consignee_address.province,
                tcustCity = waybillApplyRequest.trade_order_info_cols.consignee_address.city,
                tcustCounty = waybillApplyRequest.trade_order_info_cols.consignee_address.area,

                weight = "0",//"寄件重量"
                length = "0",//物品长度
                insure = "0",//保价，每件最高投保金额以人民币5万元为限
                cargoDesc = "内件信息",
                bigAccountDataId = waybillApplyRequest.trade_order_info_cols.order_no,
                billno = mailNo
            };
            return result;
        }
    }


    public class DEncryptHelper
    {
        private const string ENCRYPT_KEY = "forchnsoft";

        public static string Encrypt(string original)
        {
            if (string.IsNullOrEmpty(original))
            {
                return "";
            }
            return Encrypt(original, "forchnsoft");
        }

        public static string Decrypt(string cypher)
        {
            if (string.IsNullOrEmpty(cypher))
            {
                return "";
            }
            return Decrypt(cypher, "forchnsoft");
        }

        public static string Encrypt(string original, string key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(original);
            byte[] bytes2 = Encoding.UTF8.GetBytes(key);
            return Convert.ToBase64String(Encrypt(bytes, bytes2));
        }

        public static string Decrypt(string cypher, string key)
        {
            byte[] encrypted = Convert.FromBase64String(cypher);
            byte[] bytes = Encoding.UTF8.GetBytes(key);
            return Encoding.UTF8.GetString(Decrypt(encrypted, bytes));
        }

        private static byte[] Encrypt(byte[] original, byte[] key)
        {
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            tripleDESCryptoServiceProvider.Key = MakeMD5(key);
            tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
            ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor();
            byte[] result = cryptoTransform.TransformFinalBlock(original, 0, original.Length);
            cryptoTransform.Dispose();
            tripleDESCryptoServiceProvider.Dispose();
            return result;
        }

        private static byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            tripleDESCryptoServiceProvider.Key = MakeMD5(key);
            tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
            ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
            byte[] result = cryptoTransform.TransformFinalBlock(encrypted, 0, encrypted.Length);
            cryptoTransform.Dispose();
            tripleDESCryptoServiceProvider.Dispose();
            return result;
        }

        private static byte[] MakeMD5(byte[] original)
        {
            using (MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                return mD5CryptoServiceProvider.ComputeHash(original);
            }
        }
    }


    public class Message
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        //[JsonProperty(PropertyName = "message_id")]
        //public string message_id { get; set; }
        public string message_id { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        //public abstract RequestType RequestType { get; }
        public string request_type { get; }

        [Newtonsoft.Json.JsonIgnore]
        //public virtual string MessageType { get; protected set; }
        public string message_type { get; set; }

        /// <summary>
        /// 执行的动作
        /// </summary>
        //[JsonProperty(PropertyName = "action")]
        public string action { get; set; }

        public object request_params { get; set; }
    }


    public class PrintWaybillCodeModel
    {
        public string stock_code { get; set; }

        public string cp_code { get; set; }

        public string platform_code { get; set; }

        public string owner_code { get; set; }

        public string template_name { get; set; }

        public List<WaybillcodeContent> contents { get; set; }
    }

    public class WaybillcodeContent
    {
        /// <summary>
        /// 收货地址 必须输入Y
        /// </summary>
        public receive_info receive_info { get; set; }
        /// <summary>
        /// 发货地址   是否必须输入Y
        /// </summary>
        public send_info send_info { get; set; }

        //订单信息
        public trade_order_info trade_order_info { get; set; }

        //用户自定义字段
        public customer_area customer_area { get; set; }
    }

    /// <summary>
    /// 发货地址
    /// </summary>
    public class send_info
    {
        /// <summary>
        /// 省名称（一级地址）
        /// </summary>
        public string send_province { get; set; }
        /// <summary>
        /// 市名称（二级地址）
        /// </summary>
        public string send_city { get; set; }
        /// <summary>
        /// 区名称（三级地址）>
        /// </summary>
        public string send_area { get; set; }
        /// <summary>
        /// 详细地址 
        /// </summary>
        public string send_address_detail { get; set; }

        /// <summary>
        /// 发货人 必须输入Y
        /// </summary>
        public string send_name { get; set; }
        /// <summary>
        /// 发货人联系方式 必须输入Y
        /// </summary>
        public string send_phone { get; set; }


    }
    /// <summary>
    /// 收货地址
    /// </summary>
    public class receive_info
    {
        /// <summary>
        /// 省名称（一级地址）
        /// </summary>
        public string receive_province { get; set; }
        /// <summary>
        /// 市名称（二级地址）
        /// </summary>
        public string receive_city { get; set; }
        /// <summary>
        /// 区名称（三级地址）>
        public string receive_area { get; set; }

        /// <summary>
        /// 街道\镇名称（四级地址）
        /// </summary>
        public string receive_town { get; set; }

        /// <summary>
        /// 详细地址 
        /// </summary>
        public string receive_address_detail { get; set; }


        public string receive_address { get; set; }



        /// <summary>
        ///收货人联系方式 必须输入Y
        /// </summary>
        public string receive_phone { get; set; }
        /// <summary>
        /// 收货人 必须输入Y
        /// </summary>
        public string receive_name { get; set; }

    }

    public class trade_order_info
    {
        /// <summary>
        /// 运单号
        /// </summary>
        public string waybill_no { get; set; }


        /// <summary>
        /// 订单号 必须输入Y
        /// </summary>
        public string order_no { get; set; }


        /// <summary>
        /// 总金额
        /// </summary>
        public string total_fees { get; set; }

        /// <summary>
        /// 保价金额
        /// </summary>
        public string insured_fees { get; set; }

        /// <summary>
        /// 大头笔
        /// </summary>
        public string short_address { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public string freight { get; set; }


        /// <summary>
        /// 总重量
        /// </summary>
        public string total_weight { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public string pay_mothod { get; set; }

        /// <summary>
        /// 月结账号  
        /// </summary>
        public string monthly_account { get; set; }


        /// <summary>
        /// 商品信息  名称和商品CODE等信息 必须输入Y
        /// </summary>
        public List<order_items> order_items { get; set; }

        /// <summary>
        /// 集包地编码
        /// </summary>
        public string package_center_code { get; set; }

        /// <summary>
        /// 集包地名称
        /// </summary>
        public string package_center_name { get; set; }

        /// <summary>
        /// 一段码
        /// </summary>
        public string one_section_code { get; set; }
        /// <summary>
        /// 二段码
        /// </summary>
        public string two_section_code { get; set; }
        /// <summary>
        /// 三段码
        /// </summary>
        public string three_section_code { get; set; }

        /// <summary>
        /// 是否订单拦截  0否 1是
        /// </summary>
        public string is_cancel { get; set; }

        /// <summary>
        /// 错误图片
        /// </summary>
        public byte[] error_pic { get; set; }
    }

    /// <summary>
    /// 订单或者包裹里面的商品明细及数量
    /// </summary>
    public class order_items
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string item_name { get; set; }

        /// <summary>
        /// 商品条码
        /// </summary>
        public string item_code { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public string item_qty { get; set; }

    }

    /// <summary>
    /// 自定义字段
    /// </summary>
    public class customer_area
    {
        public string wave_code { get; set; }
        public string shipment_code { get; set; }
        public string package_code { get; set; }
        public string total_sku_qty { get; set; }
        public string basket_no { get; set; }
    }
}
