using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.BusinessLayer;
using System.Linq;
using Cabinet.Framework.PersistenceLayer;
using System.Data.Linq.SqlClient;
using Cabinet.UnitTest.Utility;

namespace Cabinet.UnitTest.BusinessLayer
{
    class RawResponseExt : RawResponse
    {
        public override void onResponsed()
        {
            
        }
    }

    [TestClass]
    public class UnitTest_RegionBO
    {
        public static void ClassInit(TestContext tc)
        {

        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            CabinetTreeDataContext context = ContextGrabber.grab();
            var q = from o in context.CabTree_Regions where SqlMethods.Like(o.name, "测试用公司%") select o;
            foreach (var r in q)
            {
                context.CabTree_Regions.DeleteOnSubmit(r);
            }
            context.SubmitChanges();
        }

        [TestMethod]
        public void testRegionCreate()
        {
            RawRequest q = new RawRequest();
            q.business = "region";
            q.method = "create";
            q.param.Add("测试用公司bo");
            q.param.Add("tssbo");
            RawResponse p = new RawResponseExt();
            RawContext c = new RawContext(q,p);
            BOBase b = new RegionBO(c);
            b.handleBusiness();
            Assert.AreEqual(1, p.result.Count);
            Assert.IsTrue((int)p.result.ElementAt<object>(0) > 0);
        }
    }
}
