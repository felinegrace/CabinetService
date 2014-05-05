using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CabTreeService" in both code and config file together.
    public class CabTreeService : ICabTreeService
    {
        private void logOnRequest()
        {
            Logger.info("WcfServer: Webservice =====> WcfServer.");
        }

        private void logOnResponse()
        {
            Logger.info("WcfServer: WcfServer =====> Webservice.");
            Logger.info("WcfServer: <3<3<3 Transaction Completed.");
        }
        

        public int create(string name, string shortName)
        {
            logOnRequest();
            int result = new RegionBusinessService().create(name, shortName);
            logOnResponse();
            return result;
        }
    }
}
