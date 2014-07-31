using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Demo.WebserviceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestLocalService.WorkInstructionService svc = new TestLocalService.WorkInstructionService();
            svc.wiDelivery("{\"wiGuid\":\"157FF808-ABDE-40B6-86DB-F28CD29F6EE3\",\"eqptRoomGuid\":\"C9FB1218-5CB6-461D-A7C1-C23AF3EBEEDD\",\"corrCabinetGuid\":\"5F6BA746-7444-45E8-96E5-8D398EAAC873\",\"wiOperator\":\"任建\",\"wiOperStartTime\":\"2014-06-26 00:00:00\",\"wiOperEndTime\":\"2014-06-30 00:00:00\",\"procedureList\":[{\"procedureGuid\":\"C61BC8F5-E9D8-4F6F-A02E-D7CCA0E8D11B\",\"procedure\":\"打开C12507号屏柜门\"},{\"procedureGuid\":\"6C78CD9F-DDB0-41E9-8B96-DB992162BDA7\",\"procedure\":\"检查D532号设备的状态\"},{\"procedureGuid\":\"9BB2649A-0DBD-487F-9AEA-CDCCC3C43FA1\",\"procedure\":\"把设备一位置改为后面板\"}]}");
        }
    }
}
