using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoReset
{
    public class Program
    {
        private static AutoResetEvent _workerEvent = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            Thread thread = new Thread(() =>
            {
                Proc(10);
            });
            thread.Start();

            Console.WriteLine("Waiting for Proc function");

            _workerEvent.WaitOne();

            Console.WriteLine("Starting sone process");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }

            _workerEvent.Set();

            Console.WriteLine("Worker is doing some job, Let's wait !");
           
            _workerEvent.WaitOne();

            Console.WriteLine("Completed");




        }

        private static void Proc(int v)
        {
            Console.WriteLine("Starting some function");
            Thread.Sleep(1000);
            Console.WriteLine("Okay");
            _workerEvent.Set();
            Console.WriteLine("Main thread is working. I am waiting for it");
            _workerEvent.WaitOne();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Proc : {i}");
                Thread.Sleep(1000);
            }
        }
    }
}
