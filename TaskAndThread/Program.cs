using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAndThread
{
    class CustomData
    {
        public long CreationTime;
        public int Name;
        public int ThreadNum;
    }

    class Program
    {
        static void Main(string[] args)
        {
            //TestSync();
            //TestAysnc();
            //TestThread();            
        }
        static void TestFunc()
        {
            Expression<Func<int, int>> square = (x) => x * x;
            Func<int, int, int> add = (x, y) => x + y;
            
            Console.WriteLine(add(4,5));
            Console.ReadLine();
        }
        static void TestThread()
        {
            var watch = Stopwatch.StartNew();
            var resolver = new ThreadResolver();
            string result = resolver.ExecuteMethod();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(result);
            Console.WriteLine($"Total execution time: {elapsedMs}");
            Console.ReadLine();
        }
        static void TestAysnc()
        {
            var watch = Stopwatch.StartNew();
            var asyncResolver = new AsyncResolver();
            Task<string> task = asyncResolver.ExecuteAsyncMethod();
            var result = task.Result;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(result);
            Console.WriteLine($"Total execution time: {elapsedMs}");
            Console.ReadLine();
        }

        static void TestSync()
        {
            var watch = Stopwatch.StartNew();
            var syncResolver = new SyncResolver();
            string result = syncResolver.ExecuteSyncMethod();           
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(result);
            Console.WriteLine($"Total execution time: {elapsedMs}");
            Console.ReadLine();
        }
        private static void RunTaskExample()
        {
            Task[] tasks = new Task[10];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Factory.StartNew((Object obj) => {
                    CustomData data = obj as CustomData;
                    if (data == null) return;

                    data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine("Task #{0} created at {1} on thread #{2}.",
                        data.Name, data.CreationTime, data.ThreadNum);
                }, new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks });
            }
            Task.WaitAll(tasks);
        }
        
    }
}
