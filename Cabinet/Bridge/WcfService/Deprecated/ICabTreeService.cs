using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cabinet.Bridge.WcfService
{
    /// text for class TestClass
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICabTreeService" in both code and config file together.
    [ServiceContract]
    public interface ICabTreeService
    {
        /// <summary>
        /// regionSearch is a method in the TestClass class.
        /// </summary>
        [OperationContract]
        string regionSearch(Guid regionGuid);
        /// <summary>
        /// regionSearch is a method in the TestClass class.
        /// </summary>
        [OperationContract]
        string regionCreate(string name, string shortName);
        /// <summary>
        /// regionSearch is a method in the TestClass class.
        /// </summary>
        [OperationContract]
        string regionRead();
        /// <summary>
        /// regionSearch is a method in the TestClass class.
        /// </summary>
        [OperationContract]
        string regionUpdate(Guid regionGuid, string name, string shortName);
        /// <summary>
        /// regionSearch is a method in the TestClass class.
        /// </summary>
        [OperationContract]
        string regionDelete(Guid regionGuid);

        string volClassSearch(Guid volClassGuid);
        string volClassCreate(Guid regionGuid, string name);
        string volClassRead(Guid regionGuid);
        string volClassUpdate(Guid volClassGuid, string name);
        string volClassDelete(Guid guid);
        
        string eqptRoomSearch(Guid eqptRoomGuid);
        string eqptRoomCreate(Guid volClassGuid, string name);
        string eqptRoomRead(Guid volClassGuid);
        string eqptRoomUpdate(Guid eqptRoomGuid, string name);
        string eqptRoomDelete(Guid eqptRoomGuid);

        string cabinetSearch(Guid cabinetGuid);
        string cabinetCreate(Guid eqptRoomGuid, string name, int width, int height, int depth);
        string cabinetRead(Guid eqptRoomGuid);
        string cabinetUpdate(Guid cabinetGuid, string name, int width, int height, int depth);
        string cabinetDelete(Guid cabinetGuid);

        string deviceSearch(Guid deviceGuid);
        string deviceCreate(Guid cabinetGuid, string name, 
            int x, int y, int z,
            int width, int height, int depth,
            int side);
        string deviceRead(Guid cabinetGuid);
        string deviceUpdate(Guid deviceGuid, string name, 
            int x, int y, int z,
            int width, int height, int depth,
            int side);
        string deviceDelete(Guid deviceGuid);

        string deviceAttributeSearch(Guid deviceAttributeGuid);
        string deviceAttributeCreate(Guid deviceGuid, string name, string type, string value);
        string deviceAttributeRead(Guid deviceGuid);
        string deviceAttributeUpdate(Guid deviceAttributeGuid, string name, string type, string value);
        string deviceAttributeDelete(Guid deviceAttributeGuid);
    }
}
