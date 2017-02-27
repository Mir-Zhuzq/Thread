using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadPoolingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程进行异步调用");
            AutoResetEvent asyncOpIsDone = new AutoResetEvent(false);

            ThreadPool.QueueUserWorkItem(new WaitCallback(MyAsyncOperation), asyncOpIsDone);

            Console.WriteLine("主线程执行其他任务");

            Console.WriteLine("主线程等待任务处理结束");

            asyncOpIsDone.WaitOne();

        }

        //用于生成工作任务的代理实例
        static void MyAsyncOperation(Object state)
        {
            Console.WriteLine("工作任务");

            Thread.Sleep(5000);

            //指示工作任务已经完成
            ((AutoResetEvent)state).Set();
        }
    }
}
