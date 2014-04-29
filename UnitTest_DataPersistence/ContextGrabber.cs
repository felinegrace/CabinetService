using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Data.Persistence;
using Cabinet.Data.Persistence.DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace UnitTest_DataPersistence
{
    class ContextGrabber
    {
        internal static CabinetTreeDataContext grab()
        {
            Type type = Type.GetType("Cabinet.DataPersistence.DAO.CabinetTreeDAOBase,Cabinet.DataPersistence");
            CabinetTreeDAOBase bs = Activator.CreateInstance(type , true) as CabinetTreeDAOBase;
            PrivateObject privateObject = new PrivateObject(bs,new PrivateType(type));
            return privateObject.GetFieldOrProperty("context") as CabinetTreeDataContext;
        }
    }
}
