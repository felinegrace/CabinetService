using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cabinet.Bridge.Ipc.EndPoint;
using Cabinet.Utility;

namespace Cabinet.Demo.ClientConsole
{
    class IpcClientConsole
    {
        static private int testScalar_ThreadCount = 1;
        static private int testScalar_WorkCount = 1;
        static AutoResetEvent[] handleArray = new AutoResetEvent[testScalar_ThreadCount];
        class WorkerParam
        {
            public IpcClientSync client { get; set; }
            public int workerId { get; set; }
            public WorkerParam(IpcClientSync cli, int id)
            {
                client = cli;
                workerId = id;
            }
        }
        public static void entry()
        {

            IpcClientSync c = new IpcClientSync();

            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(10, 10);
            System.Threading.WaitCallback threadfunc = new WaitCallback(work);
            for (int i = 0; i < testScalar_ThreadCount; i++)
            {
                handleArray[i] = new AutoResetEvent(false);
                WorkerParam p = new WorkerParam(c, i);
                ThreadPool.QueueUserWorkItem(threadfunc, p);
                Logger.debug("work:" + i.ToString() + "launched\n");


            }
            WaitHandle.WaitAll(handleArray);
            Logger.info("send complete.");
            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
            } while (ch.Key != ConsoleKey.Q);
        }

        static void work(object param)
        {
            WorkerParam p = param as WorkerParam;
            for (int i = 0; i < testScalar_WorkCount; i++)
            {
                string request = String.Format(@"
                    {{
                        ""business"" : ""region"",
                        ""method"" : ""create"",
                        ""param"" : 
                        [
                            ""测试用公司{0}.{1}"",
                            ""tss""
                        ]
                    }}", p.workerId, i
                );
                p.client.sendMessage(request.Replace("\r\n", ""));
            }
            handleArray[p.workerId].Set();
        }
    }
}
