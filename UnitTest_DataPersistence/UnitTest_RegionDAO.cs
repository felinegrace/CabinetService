using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Data.Linq.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.PersistenceLayer;

namespace UnitTest_DataPersistence
{
    /// <summary>
    /// Summary description for RegionDAO
    /// </summary>
    [TestClass]
    public class UnitTest_RegionDAO
    {
        public UnitTest_RegionDAO()
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
        public void performTest()
        {
            string name = "测试用公司";
            string shortName = "ts";
            RegionDAO dao = new RegionDAO();
            //test c
            int r0 = dao.c(name, shortName);
            Assert.IsTrue(r0 > 0);
            //test r
            var q = from o in dao.r() where o.id == r0 select o;
            Assert.AreEqual(1, q.Count());
            //test u
            assertRegion(q.Single(), r0, name, shortName);
            dao.u(new RegionVO{id=r0, name="测试用公司2", shortName="tts"});
            var qq = from oo in dao.r() where oo.id == r0 select oo;
            Assert.AreEqual(1, q.Count());
            //test d
            assertRegion(qq.Single(), r0, "测试用公司2", "tts");
            dao.d(r0);
            var qqq = from ooo in dao.r() where ooo.id == r0 select ooo;
            Assert.AreEqual(0, qqq.Count());
        }

        private void assertRegion(RegionVO obj , int id , string name, string shortName)
        {
            Assert.AreEqual(id, obj.id);
            Assert.AreEqual(name, obj.name);
            Assert.AreEqual(shortName, obj.shortName);
        }
    }
}
