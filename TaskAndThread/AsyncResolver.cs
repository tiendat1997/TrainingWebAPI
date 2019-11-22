using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAndThread
{
    public class AsyncResolver
    {
        public AsyncResolver()
        {

        }
        public async Task<string> ExecuteAsyncMethod()
        {
            //Task<string> runTask = RunDownloadAsync();
            Task<string> runTask = RunDownloadParallelAsync();
            string taskResult = runTask.Result;
            Console.WriteLine($"Task Result: {taskResult}");
            return $"ExecuteAsyncMethod - Done - Thread {Thread.CurrentThread.ManagedThreadId}";
        }

        public async Task<string> RunDownloadAsync()
        {
            List<string> websites = Ultility.PrepData();
            foreach (var site in websites)
            {
                WebsiteDataModel result = await Task.Run(() => Ultility.DownloadWebsite(site));
                result.Report();
            }
            return $"RunDownloadSync - Done - Thread {Thread.CurrentThread.ManagedThreadId}";
        }
        public void RunDownloadSync()
        {
            List<string> websites = Ultility.PrepData();
            foreach (var site in websites)
            {
                WebsiteDataModel result = Ultility.DownloadWebsite(site);
                result.Report();
            }
        }

        public async Task<string> RunDownloadParallelAsync()
        {
            List<string> websites = Ultility.PrepData();
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();

            for (int i = 0; i < websites.Count; i++)
            {
                var site = websites[i];
                tasks.Add(Task.Run(() => Ultility.DownloadWebsiteAsync(site)));
            }

            var results = await Task.WhenAll(tasks);
            // create a task that completed when all parameter tasks was completed

            foreach (var item in results)
            {
                item.Report();
            }
            return $"RunDownloadParallelAsync - Done - Thread {Thread.CurrentThread.ManagedThreadId}";
        }
    }
}
