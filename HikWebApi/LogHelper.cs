using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HikWebApi
{

    public class LogHelper
    {


        //private static readonly ILoggerRepository Repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
        //private static readonly ILog Loginfo = LogManager.GetLogger(Repository.Name , "logger.info");
        //private static readonly ILog LogError = LogManager.GetLogger(Repository.Name, "logger.error");

        private static ILoggerRepository repository { get; set; }

        public static void Configure(string repositoryName = "NETCoreRepository", string configFile = "log4net.config")
        {
            repository = LogManager.CreateRepository(repositoryName);
            XmlConfigurator.Configure(repository, new FileInfo(configFile));
        }


        public static void LoggerInfo(Type type, object msg)
        {

            ILog log = LogManager.GetLogger("NETCoreRepository", type);
            log.Info(msg);
        }

        public static void LoggerError(Type type, object msg)
        {
            ILog log = LogManager.GetLogger("NETCoreRepository", type);
            log.Error(msg);
        }

        //public static void LoggerError(string errorMsg)
        //{

        //    LogError.Error(errorMsg);

        //}

        //public static void LoggerInfo(string msg)
        //{
        //    Loginfo.Info(msg);
        //}


    }
}
