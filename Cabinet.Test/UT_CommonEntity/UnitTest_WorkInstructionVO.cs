using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.UnitTest.CommonEntity
{
    /// <summary>
    /// Summary description for UnitTest_WorkInstructionVO
    /// </summary>
    [TestClass]
    public class UnitTest_WorkInstructionVO
    {
        public UnitTest_WorkInstructionVO()
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
        #endregion

        [TestMethod]
        public void testWorkInstruction()
        {
            string test = @"
            {
                ""wiGuid"" : ""0A3065ED-28F2-4F75-8A35-58333FF2E78B"",
                ""eqptRoomGuid"" : ""0A3065ED-28F2-4F75-8A35-58333FF2E78B"",
                ""wiOperator"" : 123,
                ""procedureList"" : 
                [
                    {
                    ""procedureGuid"" : ""0A3065ED-28F2-4F75-8A35-58333FF2E78B"",
                    ""procedure"" : ""test1"",
                    ""corrCabinetGuid"" : ""0A3065ED-28F2-4F75-8A35-58333FF2E78B""
                    }
                ]
            }";
            WorkInstructionDeliveryVO r = WorkInstructionDeliveryVO.fromJson<WorkInstructionDeliveryVO>(test);
            Assert.AreEqual("test1", r.procedureList.ElementAt(0).procedure);

        }
    }
}
