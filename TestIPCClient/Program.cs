using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cabinet.Bridge.IPC.EndPoint;
using Cabinet.Utility;
namespace TestIPCClient
{
    class Program
    {
        static private int testScalar_ThreadCount = 10000;
        static private int testScalar_WorkCount = 10000;
        static AutoResetEvent[] handleArray = new AutoResetEvent[testScalar_ThreadCount];
        class WorkerParam
        {
            public IPCClientSync client {get;set;}
            public int workerId {get;set;}
            public WorkerParam(IPCClientSync cli,int id)
            {
                client = cli;
                workerId = id;
            }
        }
        static void Main(string[] args)
        {
            Logger.enable();
            IPCClientSync c = new IPCClientSync();
            
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(10, 10);
            System.Threading.WaitCallback threadfunc = new WaitCallback(work);
            for (int i = 0; i < testScalar_ThreadCount; i++)
            {
                handleArray[i] = new AutoResetEvent(false);
                WorkerParam p = new WorkerParam(c,i);
                ThreadPool.QueueUserWorkItem(threadfunc , p);
                Logger.debug("work:" + i.ToString() + "launched\n");
           
                
            }
            //WaitHandle.WaitAll(handleArray);
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
                string msg = String.Format("thread : {0}",p.workerId);
                string prm = String.Format("work : {0}", i);
                p.client.sendMessage(msg, prm);
            }
            handleArray[p.workerId].Set();
        }
    }
}
