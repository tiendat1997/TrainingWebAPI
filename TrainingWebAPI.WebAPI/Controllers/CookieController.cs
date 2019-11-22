using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using TrainingWebAPI.WebAPI.Ultilities;

namespace TrainingWebAPI.WebAPI.Controllers
{
    public class CookieController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var resp = new HttpResponseMessage();
            var nvc = new NameValueCollection();
            nvc["sid"] = "12345";
            nvc["token"] = "abcdef";
            nvc["theme"] = "dark blue";
            var cookie = new CookieHeaderValue("session", nvc);

            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";

            resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return resp;
        }
    }
}
