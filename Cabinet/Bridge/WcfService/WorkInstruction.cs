using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cabinet.Bridge.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WorkInstruction" in both code and config file together.
    public class WorkInstruction : IWorkInstruction
    {

        public string wiSearch(Guid wiGuid)
        {
            throw new NotImplementedException();
        }

        public string wiCreate(string name, string title, int ownerId, string procedues, string potencies)
        {
            throw new NotImplementedException();
        }

        public string wiReadByOwner(int ownerId)
        {
            throw new NotImplementedException();
        }

        public string wiUpdate(Guid wiGuid, string name, string title, int ownerId, string procedues, string potencies)
        {
            throw new NotImplementedException();
        }

        public string wiDelete(Guid wiGuid)
        {
            throw new NotImplementedException();
        }
    }
}
