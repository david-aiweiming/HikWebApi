using System;
using System.Collections.Generic;
using System.Text;

namespace HikWebApi.Model
{
    public class TaoTianHikModel
    {


        public class TaoTianCutDataRequest
        {
            public long id { get; set; }
            public string userName { get; set; }
            public string ipAddress { get; set; }
            public string passWord { get; set; }
            public string portNo { get; set; }
            public string channelNo { get; set; }
            public string waybillCode { get; set; }
            public string stockCode { get; set; }
            public long startTime { get; set; }
            public long endTime { get; set; }
            public string orderTag { get; set; }

            public string pickingListNo { get; set; }
        }

        public class TaoTianCutDataResponse
        {
            public int code { get; set; }
            public string message { get; set; }

        }
    }
}
