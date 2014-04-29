using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cabinet.Framework.PersistenceLayer;

namespace Cabinet.UnitTest.PersistenceLayer
{
    class ContextGrabber
    {
        internal static CabinetTreeDataContext grab()
        {
            Type type = Type.GetType("Cabinet.DataPersistence.DAO.CabinetTreeDAOBase,Cabinet.DataPersistence");
            DAOBase bs = Activator.CreateInstance(type , true) as DAOBase;
            PrivateObject privateObject = new PrivateObject(bs,new PrivateType(type));
            return privateObject.GetFieldOrProperty("context") as CabinetTreeDataContext;
        }
    }
}
