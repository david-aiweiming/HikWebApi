
using HWMSWEBAPI.Model;
using HWMSWEBAPI.MSSQLDAL;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject1
{
    /// <summary>
    /// 无效代码
    /// </summary>
    [TestFixture]
    public class TestClass1
    {
        [Test]
        public void TestMethod()
        {

            string timetampstr = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();


            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string kk= Convert.ToInt64(ts.TotalSeconds).ToString();

            // TODO: Add your test code here



            //DataTable dt = new DataTable();
            //dt = StorerPartnerDAL.GetWaybillShenbaoList();

            //foreach (DataRow dataRow in dt.Rows)
            //{
            //    string owners = @"JHBJ,JHCJ,JHFEN,JHGL,JHMF,JHDF,JHPLWS,JHBC,JHYC,JHYR,JHHSY,JHYB,JHWL,JHTZ,JHYT,JHJG,JHTM,JHJK,JHNT,JHSCI,JHHH,JHQJ,JHXY,JHHX,JHMY,JHGG,JHZC,JHYZL,JHSX,JHJJ,JHYT1,JHYH,JHBY,JHKN,JHWX,JHWG,JHGB,JHAS,JHZX,JHXG,JHJD-ZY,JHYHI";

            //    string[] kk = owners.Split(',');
            //    foreach (var item in kk)
            //    {
            //        BaDictionaryCodeModel baDictionaryCodeModel = new BaDictionaryCodeModel();
            //        baDictionaryCodeModel.stock_code = "HD-JHA-02";
            //        baDictionaryCodeModel.owner_code = item.Trim();
            //        baDictionaryCodeModel.platform_code = dataRow["cp_code"].ToString();
            //        baDictionaryCodeModel.cp_code = dataRow["cp_code"].ToString();
            //        baDictionaryCodeModel.dictionary_code = dataRow["dictionary_code"].ToString();
            //        baDictionaryCodeModel.dictionary_value = dataRow["dictionary_value"].ToString();
            //        baDictionaryCodeModel.dictionary_type = "1";

            //        BaDictionaryCodeDAL baDictionaryCodeDAL = new BaDictionaryCodeDAL();
            //        baDictionaryCodeDAL.Add(baDictionaryCodeModel);
            //    }

            //}

            string str = "0-A29-三线▇▇";


            int i = str.Length;
        }
    }
}
