using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cabinet.Bridge.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAttrTypeService" in both code and config file together.
    [ServiceContract]
    public interface IAttrTypeService
    {
        [OperationContract]
        string enumerationSearch(Guid enumGuid);
        string enumerationSearchByElement(string typeName, string typeElements);
        string enumerationCreate(string typeName, string typeElements);
        string enumerationUpdate(Guid enumGuid, string typeName, string typeElements);
        string enumerationDelete(Guid enumGuid);
    }
}
