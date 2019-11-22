using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAndThread
{
    public class WebsiteDataModel
    {
        public string WebsiteUrl { get; set; }
        public string WebsiteData { get; set; }

        public WebsiteDataModel()
        {

        }
        public void Report()
        {
            if (string.IsNullOrEmpty(WebsiteUrl) || string.IsNullOrEmpty(WebsiteData))
            {
                Console.WriteLine($"Null data is founded at Thread {Thread.CurrentThread.ManagedThreadId} {Environment.NewLine}");
            }
            else
            {
                Console.WriteLine($"{WebsiteUrl} downloaded: {WebsiteData.Length} characters long. {Environment.NewLine}");
            }
        }
    }
}
