using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cabinet.Bridge.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WorkInstructionService" in both code and config file together.
    class WorkInstructionService : ServiceBase, IWorkInstructionService
    {
        public string wiDelivery(string wiDeliveryObject)
        {
            return service(() => new WorkInstructionServiceBusinessImpl().delivery(wiDeliveryObject));
        }
    }
}
