using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HWMS.Core.ThermalPrints
{
    public class ThermalHelpers
    {
        public Dictionary<string, Dictionary<string, string>> thermals = new Dictionary<string, Dictionary<string, string>>();

        public ThermalHelpers()
        {
            #region 菜鸟的参数 CN
            Dictionary<string, string> cnThermal = new Dictionary<string, string>();
            cnThermal.Add("PARTNER_REF_1", "ServerUrl");
            cnThermal.Add("PARTNER_REF_8", "TemplateType");
            cnThermal.Add("REMARKS", "TemplateUrl");

            thermals.Add("CN", cnThermal);
            #endregion

            #region 拼多多的参数 PDD
            Dictionary<string, string> pddThermal = new Dictionary<string, string>();
            pddThermal.Add("PARTNER_REF_1", "ServerUrl");
            pddThermal.Add("PARTNER_REF_2", "ClientId");
            pddThermal.Add("PARTNER_REF_3", "ClientSecret");
            pddThermal.Add("PARTNER_REF_4", "AccessToken");
            pddThermal.Add("PARTNER_REF_8", "TemplateType");
            pddThermal.Add("REMARKS", "TemplateUrl");

            thermals.Add("PDD", pddThermal);
            #endregion

            #region EMS,EYB 的参数
            Dictionary<string, string> emsThermal = new Dictionary<string, string>();
            emsThermal.Add("PARTNER_REF_1", "ServerUrl");
            emsThermal.Add("PARTNER_REF_2", "MailNoBatchSize");
            emsThermal.Add("PARTNER_REF_3", "MailNoSingleSize");
            emsThermal.Add("PARTNER_REF_4", "Account");
            emsThermal.Add("PARTNER_REF_5", "Password");
            emsThermal.Add("PARTNER_REF_6", "AppKey");
            emsThermal.Add("PARTNER_REF_7", "BizType");

            thermals.Add("EMS", emsThermal);
            thermals.Add("EYB", emsThermal);
            #endregion

            #region FAST的参数
            Dictionary<string, string> fastThermal = new Dictionary<string, string>();
            fastThermal.Add("PARTNER_REF_1", "ServerUrl");
            fastThermal.Add("PARTNER_REF_2", "PartnerId");
            fastThermal.Add("PARTNER_REF_3", "Passcode");

            thermals.Add("FAST", fastThermal);
            #endregion

            #region GTO 的参数
            Dictionary<string, string> gtoThermal = new Dictionary<string, string>();
            gtoThermal.Add("PARTNER_REF_1", "ServerUrl");
            gtoThermal.Add("PARTNER_REF_2", "CustomerCode");
            gtoThermal.Add("PARTNER_REF_3", "AppKey");

            thermals.Add("GTO", gtoThermal);
            #endregion

            #region HTKY的参数
            Dictionary<string, string> htkyThermal = new Dictionary<string, string>();
            htkyThermal.Add("PARTNER_REF_1", "ServerUrl");
            htkyThermal.Add("PARTNER_REF_2", "PartnerId");
            htkyThermal.Add("PARTNER_REF_3", "PartnerKey");

            thermals.Add("HTKY", htkyThermal);
            #endregion

            #region JD,JDKD 的参数
            Dictionary<string, string> jdThermal = new Dictionary<string, string>();
            jdThermal.Add("PARTNER_REF_1", "ServerUrl");
            jdThermal.Add("PARTNER_REF_2", "CustomerCode");
            jdThermal.Add("PARTNER_REF_3", "MailNoBatchSize");
            jdThermal.Add("PARTNER_REF_4", "MailNoSingleSize");
            jdThermal.Add("PARTNER_REF_5", "AppKey");
            jdThermal.Add("PARTNER_REF_6", "AccessToken");
            jdThermal.Add("PARTNER_REF_7", "AppSecret");

            thermals.Add("JD", jdThermal);
            thermals.Add("JDKD", jdThermal);
            #endregion

            #region POSTB的参数
            Dictionary<string, string> postbThermal = new Dictionary<string, string>();
            postbThermal.Add("PARTNER_REF_1", "ServerUrl");
            postbThermal.Add("PARTNER_REF_2", "AppKey");
            postbThermal.Add("PARTNER_REF_3", "UserName");
            postbThermal.Add("PARTNER_REF_4", "MessageType");
            postbThermal.Add("PARTNER_REF_5", "Version");

            thermals.Add("POSTB", postbThermal);
            #endregion

            #region SF,SF2,SF38,SFGR,SFKD,SFYC,SFDF 的参数
            Dictionary<string, string> sfThermal = new Dictionary<string, string>();
            sfThermal.Add("PARTNER_REF_1", "ServerUrl");
            sfThermal.Add("PARTNER_REF_2", "Account");
            sfThermal.Add("PARTNER_REF_3", "Password");
            sfThermal.Add("PARTNER_REF_4", "BizType");
            sfThermal.Add("PARTNER_REF_5", "MonthlyAccount");
            sfThermal.Add("PARTNER_REF_6", "ShowDetailCount");
            sfThermal.Add("PARTNER_REF_7", "RowEllipsisHint");

            thermals.Add("SF", sfThermal);
            thermals.Add("SF2", sfThermal);
            thermals.Add("SF38", sfThermal);
            thermals.Add("SFGR", sfThermal);
            thermals.Add("SFKD", sfThermal);
            thermals.Add("SFYC", sfThermal);
            thermals.Add("SFDF", sfThermal);
            #endregion

            #region SNSF 的参数
            Dictionary<string, string> snsfThermal = new Dictionary<string, string>();
            snsfThermal.Add("PARTNER_REF_1", "ServerUrl");
            snsfThermal.Add("PARTNER_REF_2", "AppKey");
            snsfThermal.Add("PARTNER_REF_3", "AppSecret");
            snsfThermal.Add("PARTNER_REF_4", "supplierCode");

            thermals.Add("SNSF", snsfThermal);
            #endregion

            #region 申通的参数
            Dictionary<string, string> stoThermal = new Dictionary<string, string>();
            stoThermal.Add("PARTNER_REF_1", "ServerUrl");
            stoThermal.Add("PARTNER_REF_2", "MailNoUrl");
            stoThermal.Add("PARTNER_REF_3", "OrderUrl");
            stoThermal.Add("PARTNER_REF_4", "ClientName");
            stoThermal.Add("PARTNER_REF_5", "ClientPassword");
            stoThermal.Add("PARTNER_REF_6", "BranchName");
            stoThermal.Add("PARTNER_REF_7", "MethodSign");
            thermals.Add("STO", stoThermal);
            #endregion


            #region ttkdex 的参数
            Dictionary<string, string> ttkdexThermal = new Dictionary<string, string>();
            ttkdexThermal.Add("PARTNER_REF_1", "ServerUrl");
            ttkdexThermal.Add("PARTNER_REF_2", "MailNoBatchSize");
            ttkdexThermal.Add("PARTNER_REF_3", "MailNoSingleSize");

            thermals.Add("TTKDEX", ttkdexThermal);
            #endregion

            #region 圆通的参数
            Dictionary<string, string> ytoThermal = new Dictionary<string, string>();
            ytoThermal.Add("PARTNER_REF_1", "ServerUrl");
            ytoThermal.Add("PARTNER_REF_2", "CustomerCode");
            ytoThermal.Add("PARTNER_REF_3", "ParternId");
            ytoThermal.Add("PARTNER_REF_4", "ClientId");
            ytoThermal.Add("PARTNER_REF_5", "MailNoUrl");
            ytoThermal.Add("PARTNER_REF_6", "OrderUrl");
            ytoThermal.Add("PARTNER_REF_7", "MailNoBatchSize");
            ytoThermal.Add("PARTNER_REF_8", "MailNoSingleSize");

            thermals.Add("YTO", ytoThermal);
            #endregion

            #region YUNDA 的参数
            Dictionary<string, string> yundaThermal = new Dictionary<string, string>();
            yundaThermal.Add("PARTNER_REF_1", "ServerUrl");
            yundaThermal.Add("PARTNER_REF_2", "Account");
            yundaThermal.Add("PARTNER_REF_3", "Password");
            yundaThermal.Add("PARTNER_REF_4", "SyncOrderUrl");

            thermals.Add("YUNDA", yundaThermal);
            #endregion

            #region CNZTO,中通 的参数
            Dictionary<string, string> ztoThermal = new Dictionary<string, string>();
            ztoThermal.Add("PARTNER_REF_1", "ServerUrl");
            ztoThermal.Add("PARTNER_REF_2", "PartnerId");//PartnerId
            ztoThermal.Add("PARTNER_REF_3", "Passcode");//verify
            ztoThermal.Add("PARTNER_REF_4", "AppSecret");//companyId
            ztoThermal.Add("PARTNER_REF_5", "AppKey");  //Key 

            thermals.Add("ZTO", ztoThermal);
            thermals.Add("CNZTO", ztoThermal);
            #endregion

            #region VIP唯品会的参数 
            Dictionary<string, string> VIPThermal = new Dictionary<string, string>();
            VIPThermal.Add("PARTNER_REF_1", "ServerUrl");
            VIPThermal.Add("PARTNER_REF_2", "AppKey");
            VIPThermal.Add("PARTNER_REF_3", "AppSecret");
            VIPThermal.Add("PARTNER_REF_4", "AccessToken"); 
            VIPThermal.Add("PARTNER_REF_5", "VendorId"); 

            thermals.Add("VIP_JITX", VIPThermal);
            #endregion

            #region DBKD德邦快递的参数 
            Dictionary<string, string> DBKDThermal = new Dictionary<string, string>();
            DBKDThermal.Add("PARTNER_REF_1", "ServerUrl");
            DBKDThermal.Add("PARTNER_REF_2", "AppKey");
            DBKDThermal.Add("PARTNER_REF_3", "Sign");
            DBKDThermal.Add("PARTNER_REF_4", "CompanyCode");
            DBKDThermal.Add("PARTNER_REF_5", "CustomerCode");

            thermals.Add("DBKD", DBKDThermal);
            #endregion



            #region 跨越速运的参数
            Dictionary<string, string> kyeThermal = new Dictionary<string, string>();
            kyeThermal.Add("PARTNER_REF_1", "ServerUrl");
            kyeThermal.Add("PARTNER_REF_2", "AppKey");
            kyeThermal.Add("PARTNER_REF_3", "AppSecret");
            kyeThermal.Add("PARTNER_REF_4", "TokenUrl");
            kyeThermal.Add("PARTNER_REF_5", "RefreshTokenUrl");
            kyeThermal.Add("PARTNER_REF_6", "CustomerCode");
            kyeThermal.Add("PARTNER_REF_7", "platformFlag");
            kyeThermal.Add("PARTNER_REF_8", "AccessToken");
            kyeThermal.Add("PARTNER_REF_9", "RefreshToken");
            kyeThermal.Add("PARTNER_REF_10", "EffectiveDate");
            thermals.Add("KYE", kyeThermal);
            #endregion
        }

        //public Dictionary<string, string> getValue(string carrierCode)
        //{
        //    Dictionary<string, string> values = null;
        //    carrierCode = carrierCode.Trim().ToUpper();
        //    if (thermals.Keys.Contains(carrierCode))
        //    {
        //        values = thermals[carrierCode.Trim().ToUpper()];
        //    }
        //    else
        //    {
        //        LogUtil.Write(string.Format("快递公司:{0}参数未设置", carrierCode));
        //    }
        //    return values;
        //}
    }
}
