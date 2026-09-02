using HWMS.Core.ThermalPrints;
using HWMSWEBAPI.Model;
using HWMSWEBAPI.MSSQLDAL;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace NUnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }



        /// <summary>
        /// 从StorerPartner表导入到ba_defined_code   只用于获取获取快递单配置
        /// </summary>
        [Test]
        public void Test1()
        {
            //加载appSetting
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables();
            IConfiguration configuration = builder.Build();
            new AppSettingModel().Initial(configuration);
            #region 获取运单号参数配置的代码
            DataTable dt = new DataTable();
            dt = StorerPartnerDAL.GetListByOwnerCode("JHECO");
            ThermalHelpers thermalHelpers = new ThermalHelpers();
            foreach (DataRow dataRow in dt.Rows)
            {
                
                BaDictionaryCodeModel baDictionaryCodeModel = new BaDictionaryCodeModel();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "WORK_CENTER")
                    {
                        baDictionaryCodeModel.stock_code = dataRow[col.ColumnName].ToString();
                        continue;
                    }
                    if (col.ColumnName == "STORER_ID")
                    {
                        baDictionaryCodeModel.owner_code = dataRow[col.ColumnName].ToString();
                        continue;
                    }
                    if (col.ColumnName == "PARTNER_CODE")
                    {
                        baDictionaryCodeModel.cp_code = dataRow[col.ColumnName].ToString();
                        baDictionaryCodeModel.platform_code = dataRow[col.ColumnName].ToString();

                        continue;

                    }
                    if (col.ColumnName == "PARTNER_NAME" || col.ColumnName == "IS_EXPRESS")
                    {
                        
                        continue;

                    }

                    if (thermalHelpers.thermals.ContainsKey(baDictionaryCodeModel.cp_code))
                    {
                        Dictionary<string, string> cnThermalTemp = new Dictionary<string, string>();
                        thermalHelpers.thermals.TryGetValue(baDictionaryCodeModel.cp_code, out cnThermalTemp);
                        if (cnThermalTemp != null)
                        {
                            string cnThermalTemp1;
                            cnThermalTemp.TryGetValue(col.ColumnName, out cnThermalTemp1);
                            if (cnThermalTemp1 != null)
                            {
                                baDictionaryCodeModel.dictionary_code = cnThermalTemp1;
                                baDictionaryCodeModel.dictionary_value = dataRow[col.ColumnName].ToString();
                            }
                            else
                            {
                                continue;
                            }

                        }
                    }
                    else
                    {
                        break;
                    }
                    baDictionaryCodeModel.dictionary_type = "GET";
                    BaDictionaryCodeDAL baDictionaryCodeDAL = new BaDictionaryCodeDAL();
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
            #endregion 
        }
    }
}