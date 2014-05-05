using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Cabinet.Utility
{
    public class Logger
    {
        static int enabled = -1;
        //static object mutex = new object();

        //must call in main thread
        static public void enable()
        {
            if (enabled == -1)
                log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(@"Config\log4net.config"));
            enabled = 1;
        }

        static public void disable()
        {
            enabled = 0;
        }

        
        static public log4net.ILog getLogger()
        {
            /*
            if (!configured)
            {
                lock (mutex)
                {
                    if (!configured)
                    {
                        configured = true;
                        configure();
                    }
                }
            }
            */
            return log4net.LogManager.GetLogger("general");
        }
        

        static public void debug(string format , params object[] args)
        {
            if (enabled == 1)
                getLogger().DebugFormat(format, args);
        }

        static public void info(string format, params object[] args)
        {
            if (enabled == 1)
                getLogger().InfoFormat(format, args);
        }

        static public void error(string format, params object[] args)
        {
            if (enabled == 1)
                getLogger().ErrorFormat(format, args);
        }
    }
}


