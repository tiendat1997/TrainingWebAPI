using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAndThread
{
    public class ThreadResolver
    {
        public ThreadResolver()
        {

        }

        public string ExecuteMethod()
        {
            string taskResult = "";
            Thread thread = new Thread(() => { taskResult = RunDownload(); });
            thread.Start();
            //thread.Join();
            Console.WriteLine($"Task Result: {taskResult}");
            return $"ExecuteSyncMethod - Done - Thread {Thread.CurrentThread.ManagedThreadId}";
        }

        public string RunDownload()
        {
            List<string> websites = Ultility.PrepData();
            foreach (var site in websites)
            {
                WebsiteDataModel result = new WebsiteDataModel();
                Thread thread = new Thread(() => { result = Ultility.DownloadWebsite(site); });
                thread.Start();
                //thread.Join();
                result.Report();
            }
            return $"RunDownloadSync - Done - Thread {Thread.CurrentThread.ManagedThreadId}";
        }
    }
}
