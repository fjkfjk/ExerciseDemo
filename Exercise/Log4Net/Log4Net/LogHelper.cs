using log4net;
using System;
using System.Reflection;

namespace Log4NetDemo
{
    public class LogHelper
    {
        public static void InsertLog(Type type, string message, Log4NetDemo.EnumPublic.EnumLogRank rank)
        {
            ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            //ILog log = log4net.LogManager.GetLogger("LogDemo");
            switch(rank)
            {
                case EnumPublic.EnumLogRank.Fatal:
                    log.Fatal(message);
                    break;
                case EnumPublic.EnumLogRank.Error:
                    log.Error(message);
                    break;
                case EnumPublic.EnumLogRank.Warn:
                    log.Warn(message);
                    break;
                case EnumPublic.EnumLogRank.Info:
                    log.Info(message);
                    break;
                case EnumPublic.EnumLogRank.Debug:
                    log.Debug(message);
                    break;
            }
        }

        public static void InsertLog(Type type, Exception e, Log4NetDemo.EnumPublic.EnumLogRank rank)
        {
            ILog log = log4net.LogManager.GetLogger(type);
            switch (rank)
            {
                case EnumPublic.EnumLogRank.Fatal:
                    log.Fatal(e.Message, e);
                    break;
                case EnumPublic.EnumLogRank.Error:
                    log.Error(e.Message, e);
                    break;
                case EnumPublic.EnumLogRank.Warn:
                    log.Warn(e.Message, e);
                    break;
                case EnumPublic.EnumLogRank.Info:
                    log.Info(e.Message, e);
                    break;
                case EnumPublic.EnumLogRank.Debug:
                    log.Debug(e.Message, e);
                    break;
            }
        }
    }
}
