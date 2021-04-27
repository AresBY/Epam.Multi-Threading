using System;
using System.Linq;
using System.Threading.Tasks;

namespace V1.TaskMulth._02
{
    /// <summary>
    /// Write a program, which creates a chain of four Tasks. First Task – creates an array of 10 random integer.
    /// Second Task – multiplies this array with another random integer. 
    /// Third Task – sorts this array by ascending. Fourth Task – calculates the average value. All this tasks should print the values to console
    /// </summary>
    class Program
    {
        private static readonly Random Random = new Random();
        private static readonly object Lock = new object();
        static void Main()
        {
            int[] array = null;


            Task<int[]> task1 = Task.Run(() =>
            {
               array = new int[10];
               for (int i = 0; i < 10; i++)
               {
                   array[i] = Random.Next(1, 4);
               }
               ShowInformation("Task1. array is: ", array);
               return array;
            });


            int ranInteger = Random.Next(1, 4);
            Task<int[]> task2 = task1.ContinueWith(ar => ar.Result.Select(t => t * ranInteger).ToArray());
            task2.Wait();
            ShowInformation($"Task2. RandomInteger is {ranInteger}, array is: ", task2.Result);


            Task<int[]> task3 = Task.Run(() => task2.Result.OrderBy(t => t).ToArray());
            task3.Wait();
            ShowInformation($"Task3. array is: ", task3.Result);


            Task<double> task4 = Task.Run(() => task2.Result.Average());
            task4.Wait();
            ShowInformation($"Task4. average value is: ", task4.Result);


            Console.ReadLine();
        }

        private static void ShowInformation(string message, int[] array)
        {
            var str = string.Join(" ", array);
            Console.WriteLine($"{message} {str}");
        }
        private static void ShowInformation(string message, double value)
        {
            Console.WriteLine($"{message} {value}");
        }
    }
}
