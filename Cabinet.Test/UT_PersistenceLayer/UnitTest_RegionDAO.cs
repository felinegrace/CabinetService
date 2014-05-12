using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Data.Linq.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.PersistenceLayer;
using Cabinet.UnitTest.Utility;

namespace Cabinet.UnitTest.PersistenceLayer
{
    /// <summary>
    /// Summary description for RegionDAO
    /// </summary>
    [TestClass]
    public class UT_RegionDAO
    {
        public UT_RegionDAO()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        [ClassInitialize()]
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
        #endregion

        [TestMethod]
        public void testRegionDAO()
        {
            string name = "测试用公司";
            string shortName = "ts";
            RegionDAO dao = new RegionDAO();
            //test c
            Guid guid = Guid.NewGuid();
            dao.c(guid, name, shortName);
            //test s
            RegionVO rv = dao.s(guid);
            Assert.IsNotNull(rv);
            Assert.AreEqual(guid , rv.guid);
            //test r
            var q = from o in dao.r() where o.guid == guid select o;
            Assert.AreEqual(1, q.Count());
            //test u
            assertRegion(q.Single(), guid, name, shortName);
            dao.u(new RegionVO{guid = guid, name="测试用公司2", shortName="tts"});
            var qq = from oo in dao.r() where oo.guid == guid select oo;
            Assert.AreEqual(1, q.Count());
            //test d
            assertRegion(qq.Single(), guid, "测试用公司2", "tts");
            dao.d(guid);
            var qqq = from ooo in dao.r() where ooo.guid == guid select ooo;
            Assert.AreEqual(0, qqq.Count());
        }

        private void assertRegion(RegionVO obj , Guid guid , string name, string shortName)
        {
            Assert.AreEqual(guid, obj.guid);
            Assert.AreEqual(name, obj.name);
            Assert.AreEqual(shortName, obj.shortName);
        }
    }
}
