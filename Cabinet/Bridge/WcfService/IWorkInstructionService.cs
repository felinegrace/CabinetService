using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cabinet.Bridge.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWorkInstructionService" in both code and config file together.
    [ServiceContract]
    public interface IWorkInstructionService
    {
        [OperationContract]
        string wiDelivery(string wiDeliveryObject);
    }
}
