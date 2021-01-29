using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace BrainstormSessions
{
    public static class Logger
    {
        public static bool useLogs;
        private static ILog log = LogManager.GetLogger("LOGGER");

        public static ILog Log
        {
            get { return log; }
        }

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
            useLogs = ConfigurationManager.AppSettings.Get("useLogs") == "useLogs" ? true : false;
            if (useLogs)
            log.Debug("Debug info");
        }
    }
}
