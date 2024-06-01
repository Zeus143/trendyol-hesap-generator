using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trendyol
{
    internal class TrendyolAccountCreator
    {
        static StreamWriter hesaplar = new StreamWriter("accounts.txt",true)
        {
            AutoFlush = true
        };

        string tstate = string.Empty;
        string tparent = string.Empty;
        string deviceID = string.Empty;
        string pid = string.Empty;
        string uniqueid = string.Empty, tpaydid = string.Empty; //16 karakter
        string userAgent = string.Empty;
        string sid = string.Empty;
        string email = string.Empty;
        public TrendyolAccountCreator()
        {
            tstate = Guid.NewGuid().ToString().Substring(0, 16);
            tparent = Guid.NewGuid().ToString().Replace("-", "");
            deviceID = Guid.NewGuid().ToString();
            pid = Guid.NewGuid().ToString();
            uniqueid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
            tpaydid = uniqueid;
            userAgent = Http.ChromeUserAgent();
            sid = Guid.NewGuid().ToString();
            email = Email1.GetMail();
            X1();
        }
        void X1()
        {
            using (HttpRequest req = new HttpRequest())
            {
                var ti = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                req.AddHeader("newrelic", "{\"v\":[0,2],\"d\":{\"ty\":\"Mobile\",\"ac\":\"\",\"ap\":\"\",\"tr\":\"" + tparent + "\",\"id\":\"" + tstate + "\",\"ti\":" + ti + ",\"tk\":\"\"}}");
                req.AddHeader("tracestate", $"@nr=0-2---{tstate}----{ti}");
                req.AddHeader("traceparent", $"00-{tparent}-{tstate}-00");
                req.AddHeader("deviceid", deviceID);
                req.AddHeader("pid", pid);
                req.AddHeader("build", "7.19.4.784");
                req.AddHeader("platform", "Android");
                req.AddHeader("osversion", "7.1.2");
                req.AddHeader("isemulator", "false");
                req.AddHeader("x-channelid", "1");
                req.AddHeader("uniqueid", uniqueid);
                req.AddHeader("tpaydid", tpaydid);
                req.AddHeader("app-name", "Trendyol");
                req.AddHeader("lc-member", "true");
                req.AddHeader("accept-language", "tr-TR");
                req.AddHeader("x-storefront-id", "1");
                req.AddHeader("searchsegment", "70");
                req.AddHeader("gender", "F");
                req.AddHeader("segments", " ");
                req.AddHeader("x-features", "OPTIONAL_REBATE;TRENDYOLPAY_ENABLED;VAS_ENABLED;CONSUMER_LENDING_ANDROID_V3");
                req.AddHeader("user-agent", userAgent);
                req.AddHeader("x-application-id", "5");
                req.AddHeader("cl_eligible", "false");
                req.AddHeader("sid", sid);
                req.AddHeader("content-type", "application/json; charset=UTF-8");
                req.AddHeader("accept-encoding", "gzip");

                req.IgnoreProtocolErrors = true;
                req.Proxy = ProxyClient.Parse(ProxyType.HTTP, "45.155.68.129:8133:flqorbdx:Charon0140");
                var res = req.Post("https://memberpublic-sdc.trendyol.com/member-member-login-app-service/v3/register/user", "{\"guestToken\":\"\",\"preferences\":[{\"id\":0,\"isAccept\":false}],\"regulation\":{\"isConditionOfMembershipApproved\":true,\"isProtectionOfPersonalDataApproved\":true},\"user\":" +
                    "{\"email\":\"" + email + "\",\"gender\":2,\"isTyPrivacyStatementConsent\":false,\"" +
                    "password\":\"Sero@1985Sero\",\"storeFrontId\":\"1\",\"userType\":\"MEMBER\"},\"verifications\":[]}",
                    "application/json; charset=UTF-8");

                Console.WriteLine(res);

                if (res.ToString().Contains("success"))
                {
                    var code = Email1.GetVerifyLink(email);
                    X2(code);
                }
            }
        }
        void X2(string code)
        {
            using (HttpRequest req = new HttpRequest())
            {
                var ti = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                req.AddHeader("newrelic", "{\"v\":[0,2],\"d\":{\"ty\":\"Mobile\",\"ac\":\"\",\"ap\":\"\",\"tr\":\"" + tparent + "\",\"id\":\"" + tstate + "\",\"ti\":" + ti + ",\"tk\":\"\"}}");
                req.AddHeader("tracestate", $"@nr=0-2---{tstate}----{ti}");
                req.AddHeader("traceparent", $"00-{tparent}-{tstate}-00");
                req.AddHeader("deviceid", deviceID);
                req.AddHeader("pid", pid);
                req.AddHeader("build", "7.19.4.784");
                req.AddHeader("platform", "Android");
                req.AddHeader("osversion", "7.1.2");
                req.AddHeader("isemulator", "false");
                req.AddHeader("x-channelid", "1");
                req.AddHeader("uniqueid", uniqueid);
                req.AddHeader("tpaydid", tpaydid);
                req.AddHeader("app-name", "Trendyol");
                req.AddHeader("lc-member", "true");
                req.AddHeader("accept-language", "tr-TR");
                req.AddHeader("x-storefront-id", "1");
                req.AddHeader("searchsegment", "70");
                req.AddHeader("gender", "F");
                req.AddHeader("segments", " ");
                req.AddHeader("x-features", "OPTIONAL_REBATE;TRENDYOLPAY_ENABLED;VAS_ENABLED;CONSUMER_LENDING_ANDROID_V3");
                req.AddHeader("user-agent", userAgent);
                req.AddHeader("x-application-id", "5");
                req.AddHeader("cl_eligible", "false");
                req.AddHeader("sid", sid);
                req.AddHeader("content-type", "application/json; charset=UTF-8");
                req.AddHeader("accept-encoding", "gzip");
                req.IgnoreProtocolErrors = true;

                var res = req.Post("https://memberpublic-sdc.trendyol.com/member-member-login-app-service/v3/register/user", "{\"guestToken\":\"\",\"preferences\":[{\"id\":0,\"isAccept\":false}],\"regulation\":{\"isConditionOfMembershipApproved\":true,\"isProtectionOfPersonalDataApproved\":true},\"user\":" +
                    "{\"email\":\"" + email + "\",\"gender\":2,\"isTyPrivacyStatementConsent\":false,\"password\":\"Sero@1985Sero\",\"storeFrontId\":\"1\",\"userType\":\"MEMBER\"},\"verifications\":" +
                    "[{\"code\":\"" + code + "\",\"type\":\"EMAIL\"}]}",
                    "application/json; charset=UTF-8");

                Console.WriteLine(res);

                if (res.IsOK)
                {
                    WriteLine(hesaplar, res.ToString());
                }
            }
        }
        public static void WriteLine(StreamWriter streamWriter, string line)
        {
            lock (streamWriter)
            {
                streamWriter.WriteLine(line);
            }
        }
    }
}
