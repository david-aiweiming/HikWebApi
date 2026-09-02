using HWMSWEBAPI.Common;
using HWMSWEBAPI.Express;
using HWMSWEBAPI.Model;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject1
{
    [TestFixture]
    public class TestClass4
    {
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");

            //加载appSetting
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables();
            IConfiguration configuration = builder.Build();
            new AppSettingModel().Initial(configuration);

            WaybillSearchApply waybillSearchApply = new WaybillSearchApply();
            waybillSearchApply.stock_code = "DT_JYWMS1230";
            waybillSearchApply.owner_code = "";
            waybillSearchApply.cp_code = "YUNDA";
            waybillSearchApply.platform_code = "DOUYIN";
            waybillSearchApply.waybill_no = "3180040366286";
            waybillSearchApply.order_no = "XP1721010714201108887679006900";



            //string str = "0e19c22c-61be-4539-957c-2e2dbaa712c7access_token7262948f-9078-46be-8c44-d3e98289ff99app_key6845502036344866318methodlogistics.waybillApplyparam_json{"waybill_applies":[{"logistics_code":"jtexpress","track_no":"UT2900007160596"}]}timestamp2021-07-16 18:11:42v20e19c22c-61be-4539-957c-2e2dbaa712c7"



            //string str = "0e19c22c-61be-4539-957c-2e2dbaa712c7access_token7262948f-9078-46be-8c44-d3e98289ff99app_key6845502036344866318methodlogistics.waybillApplyparam_json{\"waybill_applies\":[{\"logistics_code\":\"jtexpress\",\"track_no\":\"UT2900007160596\"}]}timestamp2021-07-16 18:11:42v20e19c22c-61be-4539-957c-2e2dbaa712c7";

    
            //string strSign = EncodingHelper.MD5Hash(str);




            DOUYIN dOUYIN = new DOUYIN();

           

            dOUYIN.DouYinSearchMailNo(waybillSearchApply);
        }
    }
}
