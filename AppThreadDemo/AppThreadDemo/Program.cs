using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AppThreadDemo
{
    class Program
    {
        static public void threadMethod()
        {
            try
            {
                Console.WriteLine("\n 执行threadMethod 的线程的代码是：{0}", Thread.CurrentThread.GetHashCode().ToString());
                //休眠1秒
                Thread.Sleep(1000);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("捕获到异常：{0}", ex.ToString());
            }
            finally
            {
                Console.WriteLine("threadMethod执行结束");
            }
            
        }

        static public void demoThreadCreate()
        {
            Console.WriteLine("\n 执行demoThreadCreate 的线程代码是：{0}", Thread.CurrentThread.GetHashCode().ToString());

            //实例化
            ThreadStart entry = new ThreadStart(Program.threadMethod);

            //生成线程实例
            Thread thread = new Thread(entry);

            //终止线程
            //Console.WriteLine("终止线程");
            //thread.Abort();


            //启动线程
            Console.WriteLine("启动线程");
            thread.Start();

            //主线程休眠0.5秒，等待子线程运行
            //这里等待0.5s子线程最后打印结束的代码没有时间执行，如果延长等待的时间，两个线程的结束信息均可以打印
            Thread.Sleep(500);

            //终止线程
            //thread.Abort();
            thread.Interrupt();
            Console.WriteLine("主线程结束");

            //等待线程结束
            //thread.Join();
            //Console.WriteLine("主线程结束");
        }

        static void Main(string[] args)
        {
            demoThreadCreate();
            
        }
    }
}
