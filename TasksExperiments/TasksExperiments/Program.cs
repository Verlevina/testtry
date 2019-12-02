using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TasksExperiments
{
    class Program
    {
        public static void Operation(CancellationToken token)
        {
            for (int i = 0; i <= Int64.MaxValue - 1; i++)
            {
                if (i % 100 == 0)
                {
                    Console.WriteLine(i + " " + Thread.CurrentThread.ManagedThreadId);
                    throw new Exception("just exception");
                }

                token.ThrowIfCancellationRequested();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            var cancellation = new CancellationTokenSource();
            var task = new Task(
                () => 
                {
                    Console.WriteLine("taskStarted " + Thread.CurrentThread.ManagedThreadId);

                    try
                    {
                        Operation(cancellation.Token);
                    }
                    catch (OperationCanceledException exc)
                    {
                        Console.WriteLine("was canceled");
                    }
                });
            Console.WriteLine("taskStatus " + task.Status.ToString());
            task.Start();

            Console.WriteLine("taskStatus " + task.Status.ToString());

            Thread.Sleep(10);
            cancellation.Cancel();

            Console.WriteLine("taskStatus " + task.Status.ToString());

            //task.Wait();

            Console.WriteLine("taskStatus " + task.Status.ToString());

            Console.ReadKey();
        }
    }
}
