using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cabinet.Framework.CommonEntity;
using Cabinet.Utility;

namespace Cabinet.UnitTest.CommonEntity
{
    [TestClass]
    public class UT_RawRequest
    {
        [TestMethod]
        public void testRawRequest()
        {
            string test = @"
            {
                ""method"" : ""test"",
                ""param"" : 
                [
                    ""test1"",
                    ""test2"",
                    3
                ]
            }
            ";
            BusinessRequest r = BusinessRequest.fromJson<BusinessRequest>(test);
            Assert.AreEqual("test", r.method);
            Assert.AreEqual(3,r.param.Count);
            Assert.AreEqual("test1", r.param[0]);
            Assert.AreEqual("test2", r.param[1]);
            Assert.AreEqual(3L, r.param[2]);

        }
    }
}
