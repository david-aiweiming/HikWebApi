using System;
using System.Collections.Generic;
using System.Text;

namespace HikWebApi.Model
{
    public class HikModel
    {
        public class HikSetOsdRequest
        {
            public string userName { get; set; }
            public string ipAddress { get; set; }
            public string passWord { get; set; }
            public string portNo { get; set; }
            public string channelNo { get; set; }
            public string osdMessage { get; set; }
            public string waybillCode { get; set; }
            public string stockCode { get; set; }
            public string orderTag { get; set; }

        }



        public class HikSetOsdResponse
        {
            public int code { get; set; }
            public Data data { get; set; }
            public string message { get; set; }
            
        }


        public class Data
        {
            public long dateTime { get; set; }
        }


    }
}
