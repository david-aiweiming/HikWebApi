using HWMSWEBAPI.DBUtility;
using HWMSWEBAPI.Express;
using HWMSWEBAPI.IDAL;
using HWMSWEBAPI.Model;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject1
{
    [TestFixture]
    public class TestClass5
    {
        [Test]
        public void TestMethod()
        {
            //加载appSetting
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables();
            IConfiguration configuration = builder.Build();
            new AppSettingModel().Initial(configuration);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ba_waybill_log where order_no='4812079649051139955'");
            DataSet ds = DbHelperMySQL.Query(strSql.ToString());
            DataTable dt = ds.Tables[0];
            ISeWaybillResponseDAL seWaybillResponseDAL = DataAccess.CreateSeWaybillResponseDAL();
            foreach (DataRow row in dt.Rows)
            {

                var request = Newtonsoft.Json.JsonConvert.DeserializeObject<waybill_apply_request>(row["log_remark"].ToString());

                PDD pDD = new PDD();
                pDD.GetWayBillCode(request);

            }
        }
    }
}
