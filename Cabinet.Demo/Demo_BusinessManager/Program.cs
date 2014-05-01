using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Framework.BusinessLayer;
using Cabinet.Framework.CommonEntity;

namespace Demo_BusinessManager
{
    class RawResponseExt : RawResponse
    {
        public override void onResponsed()
        {

        }
    }

    class Program
    {
        static void addMaterial(BusinessManager m)
        {
            for (int i = 0; i < 100; i++)
            {
                RawRequest q = new RawRequest();
                q.business = "region";
                q.method = "create";
                q.param.Add("测试用公司bo" + i);
                q.param.Add("tssbo" + i);
                RawResponse p = new RawResponseExt();
                BusinessContext c = new BusinessContext(q, p);

                m.postRequest(c);
            }
        }
        static void Main(string[] args)
        {
            Logger.enable();
            BusinessManager m = new BusinessManager();
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
