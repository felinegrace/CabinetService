using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cabinet.Bridge.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWorkInstruction" in both code and config file together.
    [ServiceContract]
    public interface IWorkInstruction
    {
        string wiSearch(Guid wiGuid);
        string wiCreate(string name, string title, int ownerId, string procedues, string potencies);
        string wiReadByOwner(int ownerId);
        string wiUpdate(Guid wiGuid, string name, string title, int ownerId, string procedues, string potencies);
        string wiDelete(Guid wiGuid);
    }
}
