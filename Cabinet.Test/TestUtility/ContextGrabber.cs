using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cabinet.Framework.PersistenceLayer;

namespace Cabinet.UnitTest.Utility
{
    public class ContextGrabber
    {
        public static CabinetTreeDataContext grab()
        {
            Type type = Type.GetType("Cabinet.Framework.PersistenceLayer.DAOBase,Cabinet.Framework.PersistenceLayer");
            if (type == null)
                return null;
            DAOBase bs = Activator.CreateInstance(type , true) as DAOBase;
            PrivateObject privateObject = new PrivateObject(bs,new PrivateType(type));
            return privateObject.GetFieldOrProperty("context") as CabinetTreeDataContext;
        }
    }
}
