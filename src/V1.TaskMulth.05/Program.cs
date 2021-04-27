using System;
using System.Threading;

namespace V1.TaskMulth._05
{
    /// <summary>
    /// Write a program which recursively creates 10 threads. Each thread should be with the same body and receive a state with integer number, 
    /// decrement it, print and pass as a state into the newly created thread. Use ThreadPool class for this task and Semaphore for waiting threads.
    /// </summary>
    class Program
    {
        private static readonly Semaphore semaphore = new Semaphore(2, 2);

        static void Main()
        {
            CreateThread();
            Console.ReadLine();
        }

        private static void CreateThread(int number = 10)
        {
            ThreadBodyMethod(number);
        }

        private static void ThreadBodyMethod(object state)
        {
            semaphore.WaitOne();
            int number = (int)state;
            if (number > 0)
            {
                number--;
                Console.WriteLine(number);
                ThreadPool.QueueUserWorkItem(ThreadBodyMethod, number);
                Thread.Sleep(1000);
            }
            semaphore.Release();
        }
    }
}
