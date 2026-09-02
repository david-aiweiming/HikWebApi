using HWMSWEBAPI.Model;
using HWMSWEBAPI.MySQLDAL;
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

    ///申报系统的WEB.CONFIG基础数据的导入到MS SQL
    public class TestClass2
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
            DataTable dt = new DataTable();
            dt = StorerPartnerDAL.GetWaybillShenbaoListSN();
            foreach (DataRow dataRow in dt.Rows)
            {
                //string owners = @"JHBJ,JHCJ,JHFEN,JHGL,JHMF,JHDF,JHPLWS,JHBC,JHYC,JHYR,JHHSY,JHYB,JHWL,JHTZ,JHYT,JHJG,JHTM,JHJK,JHNT,JHSCI,JHHH,JHQJ,JHXY,JHHX,JHMY,JHGG,JHZC,JHYZL,JHSX,JHJJ,JHYT1,JHYH,JHBY,JHKN,JHWX,JHWG,JHGB,JHAS,JHZX,JHXG,JHJD-ZY,JHYHI";
                string owners = @"JHDD";
                string[] kk = owners.Split(',');
                foreach (var item in kk)
                {
                    BaDictionaryCodeModel baDictionaryCodeModel = new BaDictionaryCodeModel();
                    baDictionaryCodeModel.stock_code = "HD-JHA-02";
                    baDictionaryCodeModel.owner_code = item.Trim();
                    baDictionaryCodeModel.platform_code = dataRow["cp_code"].ToString();
                    baDictionaryCodeModel.cp_code = "HTKY";
                    baDictionaryCodeModel.dictionary_code = dataRow["dictionary_code"].ToString();
                    baDictionaryCodeModel.dictionary_value = dataRow["dictionary_value"].ToString();
                    baDictionaryCodeModel.dictionary_type = "UPLOAD";
                    HWMSWEBAPI.MSSQLDAL.BaDictionaryCodeDAL baDictionaryCodeDAL = new HWMSWEBAPI.MSSQLDAL.BaDictionaryCodeDAL();

                    if (baDictionaryCodeDAL.Exists(baDictionaryCodeModel))
                    {
                        baDictionaryCodeDAL.Update(baDictionaryCodeModel);
                    }
                    else
                    {
                        baDictionaryCodeDAL.Add(baDictionaryCodeModel);
                    }
                  
                }

            }
        }
    }
}
