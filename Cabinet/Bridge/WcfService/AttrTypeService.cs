using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cabinet.Bridge.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AttrTypeService" in both code and config file together.
    public class AttrTypeService : IAttrTypeService
    {

        public string enumerationSearch(Guid enumGuid)
        {
            throw new NotImplementedException();
        }

        public string enumerationSearchByElement(string typeName, string typeElements)
        {
            throw new NotImplementedException();
        }

        public string enumerationCreate(string typeName, string typeElements)
        {
            throw new NotImplementedException();
        }

        public string enumerationUpdate(Guid enumGuid, string typeName, string typeElements)
        {
            throw new NotImplementedException();
        }

        public string enumerationDelete(Guid enumGuid)
        {
            throw new NotImplementedException();
        }
    }
}
