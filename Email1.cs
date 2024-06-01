using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Leaf.xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Trendyol
{
    internal class Email1
    {
        /// <summary>
        /// {"result":{"current":{"mailbox":"anitmesi@email1.io","hostname":"email1.io","name":"anitmesi","expiredTime":"2024-05-23T23:07:12+00:00"},"list":[]},"error":null}
        /// </summary>
        /// <returns></returns>
        public static string GetMail()
        {
            using (HttpRequest req = new HttpRequest())
            {
                req.AddHeader("authorization", "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MTY0OTA0ODkzODQsInNpZCI6IkhhT0FtQ1F5dWMiLCJlbnRpdHkiOiJ1c2VyIn0.CTMD_ECsza0QrkEKjdi9Suma4ql9EhQ5sHZasjm_knIvo4S6yCsKFMnQvTjGx3AoyGzs4csH1ZnPUEuBmlR9Qg");
                req.AddHeader("user-agent", Http.RandomUserAgent());
                req.IgnoreProtocolErrors = true;
                var res = req.Post("https://email1.io/api/v1/mailbox/refresh", "{}", "application/json");

                return JsonConvert.DeserializeObject<JObject>(res.ToString())["result"]["current"]["mailbox"].ToString();

            }

        }
        public static string GetVerifyLink(string email)
        {
            using (HttpRequest req = new HttpRequest())
            {
                string res = string.Empty; 
                while (!res.Contains("Trendyol"))
                {
                    req.AddHeader("authorization", "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MTY0OTA0ODkzODQsInNpZCI6IkhhT0FtQ1F5dWMiLCJlbnRpdHkiOiJ1c2VyIn0.CTMD_ECsza0QrkEKjdi9Suma4ql9EhQ5sHZasjm_knIvo4S6yCsKFMnQvTjGx3AoyGzs4csH1ZnPUEuBmlR9Qg");
                    req.AddHeader("user-agent", Http.RandomUserAgent());
                    req.IgnoreProtocolErrors = true;
                    res = req.Get($"https://email1.io/api/v1/mail/preview?mailbox={email.Split('@')[0]}&length=20").ToString();
                    Thread.Sleep(1000);
                }
                var parse = res.Substring("KOD: ", "\",");
                return parse;

            }

        }
    }
}
