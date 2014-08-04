using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cabinet.Bridge.EqptRoomComm.Protocol.Message;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Bridge.EqptRoomComm.Protocol.Parser;
using Cabinet.Utility;

namespace UT_EqptRoomComm
{
    [TestClass]
    public class UnitTest_Message
    {
        [TestMethod]
        public void TestMessaging()
        {
            Register o = new Register();
            o.eqptRoomGuid = new Guid("0A3065ED-28F2-4F75-8A35-58333FF2E78B");
            RegisterMessage message = new RegisterMessage(o);
            Assert.AreEqual("register", message.verb);
            Assert.AreEqual("{\"eqptRoomGuid\":\"0a3065ed-28f2-4f75-8a35-58333ff2e78b\"}", message.payload);
            Assert.AreEqual("register\r\n{\"eqptRoomGuid\":\"0a3065ed-28f2-4f75-8a35-58333ff2e78b\"}\r\n", message.rawMessage());
            byte[] b = System.Text.Encoding.ASCII.GetBytes(message.rawMessage());
            MessageFormatParser parser = new MessageFormatParser(new DescriptorReference(b,b.Length));
            Assert.AreEqual("register", parser.verb());
            Register oo = parser.parseAs<Register>();
            Assert.AreEqual(o.eqptRoomGuid, oo.eqptRoomGuid);
        }
    }
}
