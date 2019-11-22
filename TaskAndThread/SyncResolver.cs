using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAndThread
{
    public class SyncResolver
    {
        public SyncResolver()
        {

        }

        public string ExecuteSyncMethod()
        {
            string taskResult = RunDownloadSync();
            Console.WriteLine($"Task Result: {taskResult}");
            return $"ExecuteSyncMethod - Done - Thread {Thread.CurrentThread.ManagedThreadId}";
        }

        public string RunDownloadSync()
        {
            List<string> websites = Ultility.PrepData();
            foreach (var site in websites)
            {
                WebsiteDataModel result = Ultility.DownloadWebsite(site);
                result.Report();
            }
            return $"RunDownloadSync - Done - Thread {Thread.CurrentThread.ManagedThreadId}";
        }
    }
}
