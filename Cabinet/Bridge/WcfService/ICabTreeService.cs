using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cabinet.Bridge.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICabTreeService" in both code and config file together.
    [ServiceContract]
    public interface ICabTreeService
    {
        [OperationContract]
        int create(string name, string shortName);
    }
}
