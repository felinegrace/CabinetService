using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.BusinessLayer;
using System.Linq;
using Cabinet.Framework.PersistenceLayer;
using System.Data.Linq.SqlClient;
using Cabinet.UnitTest.Utility;
using System.Collections.Generic;
using Cabinet.Utility;

namespace Cabinet.UnitTest.BusinessLayer
{
    class RawResponseExt : BusinessResponse
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
        public void testRegionBO()
        {
            BusinessRequest q = new BusinessRequest();
            q.business = "region";
            q.method = "create";
            q.param.Add("测试用公司bo");
            q.param.Add("tssbo");
            BusinessResponse p = new RawResponseExt();
            BusinessContext c = new BusinessContext(q,p);
            BOBase b = new RegionBO(c);
            b.handleBusiness();
            Assert.AreEqual(1, p.result.Count);
            Guid createdGuid = (Guid)p.result.ElementAt<object>(0);
            Assert.IsTrue(createdGuid.Equals(Guid.Empty) == false);

            BusinessRequest qq = new BusinessRequest();
            qq.business = "region";
            qq.method = "search";
            qq.param.Add(createdGuid);
            BusinessResponse pp = new RawResponseExt();
            BusinessContext cc = new
            BusinessContext(qq, pp);
            BOBase bb = new RegionBO(cc);
            bb.handleBusiness();
            Assert.AreEqual(3, pp.result.Count);
            string name = pp.result.ElementAt<object>(1) as string;
            string shortName = pp.result.ElementAt<object>(2) as string;
            Assert.AreEqual("测试用公司bo", name);
            Assert.AreEqual("tssbo", shortName);

            BusinessRequest qqq = new BusinessRequest();
            qqq.business = "region";
            qqq.method = "read";
            BusinessResponse ppp = new RawResponseExt();
            BusinessContext ccc = new
            BusinessContext(qqq, ppp);
            BOBase bbb = new RegionBO(ccc);
            bbb.handleBusiness();
            Assert.AreEqual(1, ppp.result.Count);
            List<string> list = ppp.result.ElementAt<object>(0) as List<string>;
            Assert.IsTrue(list.Contains(createdGuid.ToString()));

            BusinessRequest qqqq = new BusinessRequest();
            qqqq.business = "region";
            qqqq.method = "update";
            qqqq.param.Add(createdGuid);
            qqqq.param.Add("测试用公司bo改");
            qqqq.param.Add("tssbo2");
            BusinessResponse pppp = new RawResponseExt();
            BusinessContext cccc = new BusinessContext(qqqq, pppp);
            BOBase bbbb = new RegionBO(cccc);
            bbbb.handleBusiness();
            Assert.AreEqual(0, pppp.result.Count);

            pp.result.Clear();
            bb.handleBusiness();
            Assert.AreEqual(3, pp.result.Count);
            string name2 = pp.result.ElementAt<object>(1) as string;
            string shortName2 = pp.result.ElementAt<object>(2) as string;
            Assert.AreEqual("测试用公司bo改", name2);
            Assert.AreEqual("tssbo2", shortName2);

            BusinessRequest qqqqq = new BusinessRequest();
            qqqqq.business = "region";
            qqqqq.method = "delete";
            qqqqq.param.Add(createdGuid);
            BusinessResponse ppppp = new RawResponseExt();
            BusinessContext ccccc = new BusinessContext(qqqqq, ppppp);
            BOBase bbbbb = new RegionBO(ccccc);
            bbbbb.handleBusiness();
            Assert.AreEqual(0, ppppp.result.Count);

            pp.result.Clear();
            int a = 1;
            try
            {
                bb.handleBusiness();
            }
            catch(BOException)
            {
                a = 2;
            }
            Assert.AreEqual(2, a);
            
        }

    }
}
