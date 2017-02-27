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

        //用于生成工作任务的代理实例
        static void MyAsyncOperation(Object state)
        {
            Console.WriteLine("当前线程的代码：{0}", Thread.CurrentThread.GetHashCode().ToString());
            Console.WriteLine("当前应用域的名字为：{0}", AppDomain.CurrentDomain.FriendlyName);

            Thread.Sleep(1000);

            //指示工作任务已经完成
            ((AutoResetEvent)state).Set();
        }

        static public void demoThreadPool()
        {
            Console.WriteLine("当前线程的代码：{0}", Thread.CurrentThread.GetHashCode().ToString());
            Console.WriteLine("当前应用域的名字为：{0}", AppDomain.CurrentDomain.FriendlyName);

            //往线程池中添加两个工作
            AutoResetEvent asyncOpIsDone1 = new AutoResetEvent(false);
            AutoResetEvent asyncOpIsDone2 = new AutoResetEvent(false);

            ThreadPool.QueueUserWorkItem(new WaitCallback(Program.MyAsyncOperation), asyncOpIsDone1);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Program.MyAsyncOperation), asyncOpIsDone2);

            //构造等待对象数组
            WaitHandle[] handels = new WaitHandle[2];
            handels[0] = asyncOpIsDone1;
            handels[1] = asyncOpIsDone2;

            //等待两个工作都执行结束
            WaitHandle.WaitAll(handels);
        }

        static void Main(string[] args)
        {
            demoThreadPool();
        }

        
    }
}
