using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace TrainingWebAPI.WebAPI.Ultilities
{
    public class MyMessageResult : IHttpActionResult
    {
        private string message;
        private HttpRequestMessage request;
        public MyMessageResult(string message, HttpRequestMessage request)
        {
            this.message = message;
            this.request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(message),
                RequestMessage = request
            };
            return Task.FromResult(response);
        }
    }
}