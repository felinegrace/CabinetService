using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Framework.BusinessLayer;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Demo.BusinessManagerConsole
{
    class RawResponseExt : BusinessResponse
    {
        public override void onResponsed()
        {

        }
    }

    class Program
    {
        static void addMaterial(BusinessServer m)
        {
            for (int i = 0; i < 100; i++)
            {
                BusinessRequest q = new BusinessRequest();
                q.business = "region";
                q.method = "create";
                q.param.Add("测试用公司bo" + i);
                q.param.Add("tssbo" + i);
                BusinessResponse p = new RawResponseExt();
                BusinessContext c = new BusinessContext(q, p);

                m.postRequest(c);
            }
        }
        static void Main(string[] args)
        {
            Logger.enable();
            BusinessServer m = new BusinessServer();
            addMaterial(m);

            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
                switch(ch.Key)
                {
                    case ConsoleKey.A:
                        addMaterial(m);
                        break;
                    case ConsoleKey.S:
                        m.start();
                        break;
                    case ConsoleKey.T:
                        m.stop();
                        break;
                }
            } while (ch.Key != ConsoleKey.Q);

            //CabinetTreeDataContext context = ContextGrabber.grab();
            //var q = from o in context.CabTree_Regions where SqlMethods.Like(o.name, "测试用公司%") select o;
            //foreach (var r in q)
            //{
            //    context.CabTree_Regions.DeleteOnSubmit(r);
            //}
            //context.SubmitChanges();
        }
    }
}
