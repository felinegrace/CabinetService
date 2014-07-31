using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Cabinet.Utility;


namespace Cabinet.Bridge.WcfService
{
    
    class CabTreeService : ServiceBase, ICabTreeService
    {
        public string regionCreate(string name, string shortName)
        {
            return service(() => new RegionBusinessService().create(name, shortName));
        }

        public string regionSearch(Guid regionGuid)
        {
            return service(() => new RegionBusinessService().search(regionGuid));
        }

        public string regionRead()
        {
            return service(() => new RegionBusinessService().read());
        }

        public string regionUpdate(Guid regionGuid, string name, string shortName)
        {
            return service(() => new RegionBusinessService().update(regionGuid, name, shortName));
        }

        public string regionDelete(Guid regionGuid)
        {
            return service(() => new RegionBusinessService().delete(regionGuid));
        }

        public string volClassSearch(Guid volClassGuid)
        {
            throw new NotImplementedException();
        }

        public string volClassCreate(Guid regionGuid, string name)
        {
            throw new NotImplementedException();
        }

        public string volClassRead(Guid regionGuid)
        {
            throw new NotImplementedException();
        }

        public string volClassUpdate(Guid volClassGuid, string name)
        {
            throw new NotImplementedException();
        }

        public string volClassDelete(Guid guid)
        {
            throw new NotImplementedException();
        }

        public string eqptRoomSearch(Guid eqptRoomGuid)
        {
            throw new NotImplementedException();
        }

        public string eqptRoomCreate(Guid volClassGuid, string name)
        {
            throw new NotImplementedException();
        }

        public string eqptRoomRead(Guid volClassGuid)
        {
            throw new NotImplementedException();
        }

        public string eqptRoomUpdate(Guid eqptRoomGuid, string name)
        {
            throw new NotImplementedException();
        }

        public string eqptRoomDelete(Guid eqptRoomGuid)
        {
            throw new NotImplementedException();
        }

        public string cabinetSearch(Guid cabinetGuid)
        {
            throw new NotImplementedException();
        }

        public string cabinetCreate(Guid eqptRoomGuid, string name, int width, int height, int depth)
        {
            throw new NotImplementedException();
        }

        public string cabinetRead(Guid eqptRoomGuid)
        {
            throw new NotImplementedException();
        }

        public string cabinetUpdate(Guid cabinetGuid, string name, int width, int height, int depth)
        {
            throw new NotImplementedException();
        }

        public string cabinetDelete(Guid cabinetGuid)
        {
            throw new NotImplementedException();
        }

        public string deviceSearch(Guid deviceGuid)
        {
            throw new NotImplementedException();
        }

        public string deviceCreate(Guid cabinetGuid, string name, int x, int y, int z, int width, int height, int depth, int side)
        {
            throw new NotImplementedException();
        }

        public string deviceRead(Guid cabinetGuid)
        {
            throw new NotImplementedException();
        }

        public string deviceUpdate(Guid deviceGuid, string name, int x, int y, int z, int width, int height, int depth, int side)
        {
            throw new NotImplementedException();
        }

        public string deviceDelete(Guid deviceGuid)
        {
            throw new NotImplementedException();
        }

        public string deviceAttributeSearch(Guid deviceAttributeGuid)
        {
            throw new NotImplementedException();
        }

        public string deviceAttributeCreate(Guid deviceGuid, string name, string type, string value)
        {
            throw new NotImplementedException();
        }

        public string deviceAttributeRead(Guid deviceGuid)
        {
            throw new NotImplementedException();
        }

        public string deviceAttributeUpdate(Guid deviceAttributeGuid, string name, string type, string value)
        {
            throw new NotImplementedException();
        }

        public string deviceAttributeDelete(Guid deviceAttributeGuid)
        {
            throw new NotImplementedException();
        }
    }
}
