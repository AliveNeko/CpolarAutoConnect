using System.Net;
using CpolarAutoConnect.Core.Entity;
using CpolarAutoConnect.Core.Exception;
using HtmlAgilityPack;

namespace CpolarAutoConnect.Core.Util;

public static class CpolarStatusUtil
{
    public static async Task<(List<string>, List<List<string>>)> GetStatus()
    {
        var result = (new List<string>(), new List<List<string>>());

        var coreSetting = SettingUtil.GetSetting<CoreSetting>();

        if (coreSetting == null)
        {
            throw new CpolarException("找不到登录设定");
        }

        const string UrlStatus = "https://dashboard.cpolar.com/status";
        const string UrlLogin = "https://dashboard.cpolar.com/login";

        HttpClientHandler httpClientHandler = new HttpClientHandler
        {
            // 禁止自动重定向
            AllowAutoRedirect = false,
            // 使用 cookie
            UseCookies = true,
        };

        const string SessionFileName = "session";

        if (File.Exists(SessionFileName))
        {
            var sessionValue = File.ReadAllText(SessionFileName);

            httpClientHandler.CookieContainer.Add(new CookieCollection()
            {
                new Cookie()
                {
                    Domain = ".dashboard.cpolar.com",
                    Path = "/",
                    Name = "session",
                    Value = sessionValue,
                },
            });
        }


        HttpClient httpClient = new HttpClient(httpClientHandler);
        // 这些可能没啥用，不过以防万一我还是照着chrome的请求写上去了一些
        httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36");
        httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
        httpClient.DefaultRequestHeaders.Add("accept-language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");

        try
        {
            HttpResponseMessage statusRes = await httpClient.GetAsync(UrlStatus);

            if (statusRes.IsSuccessStatusCode)
            {
                // 成功了代表已经登录过了
            }
            else if (statusRes.StatusCode == HttpStatusCode.Redirect)
            {
                // 未登录会跳转到登录页面
                foreach (Cookie cookie in httpClientHandler.CookieContainer.GetAllCookies())
                {
                    cookie.Expires = DateTime.Now.Subtract(TimeSpan.FromDays(1));
                }

                HttpResponseMessage loginGetRes = await httpClient.GetAsync(UrlLogin);

                // 从登录页面获取给定的session cookie
                // how to get cookie from set-cookie header easy?
                List<string> setCookieList =
                    loginGetRes.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value.ToList();

                // the cookie should be like this
                // session=8ca896c6-1aa4-43a7-8ee0-3993d3cc8489; Path=/; Domain=dashboard.cpolar.com; Expires=Wed, 21 Aug 2047 17:00:05 GMT; Max-Age=784478367; HttpOnly; SameSite=Lax
                if (setCookieList.Count() > 0)
                {
                    var split = setCookieList.First().Split(";");
                    Cookie cookie = new Cookie()
                    {
                        Expires = DateTime.Now.AddYears(1),
                        Domain = "dashboard.cpolar.com",
                        Path = "/",
                    };
                    foreach (var s in split)
                    {
                        if (s.Contains("session") && s.Contains("="))
                        {
                            cookie.Name = "session";
                            cookie.Value = s.Trim().Split("=")[1];
                        }
                    }

                    if (string.IsNullOrEmpty(cookie.Value))
                    {
                        throw new CpolarException("获取登录session失败");
                    }
                    else
                    {
                        File.WriteAllText(SessionFileName, cookie.Value);
                    }

                    httpClientHandler.CookieContainer.Add(cookie);
                }
                else
                {
                    throw new CpolarException("获取set-cookie错误");
                }

                var loginPostRes = await httpClient.PostAsync(UrlLogin, new FormUrlEncodedContent(
                    new[]
                    {
                        new KeyValuePair<string, string>("login", coreSetting.LoginName),
                        new KeyValuePair<string, string>("password", coreSetting.Password),
                    }));

                if (loginPostRes.StatusCode == HttpStatusCode.Redirect)
                {
                    // 重定向代表登录成功（大概）
                }
                else
                {
                    throw new CpolarException("登录失败，请检查用户名和密码");
                }

                statusRes = await httpClient.GetAsync(UrlStatus);
            }


            if (statusRes.IsSuccessStatusCode)
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(await statusRes.Content.ReadAsStringAsync());

                const string XPath = "//*[@id=\"dashboard\"]/div/div[2]/div[2]/table";
                HtmlNode tableNode = htmlDoc.DocumentNode.SelectSingleNode(XPath);

                if (tableNode == null)
                {
                    throw new CpolarException("查找隧道表格失败，请确认是否已建立隧道");
                }

                List<string> titleList = result.Item1;
                List<List<string>> valueList = result.Item2;

                var headTrNode =
                    htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"dashboard\"]/div/div[2]/div[2]/table/thead/tr");
                if (headTrNode != null && headTrNode.ChildNodes.Count > 0)
                {
                    var thNodes = headTrNode.ChildNodes.Where(s => s.Name == "th");
                    foreach (var thNode in thNodes)
                    {
                        titleList.Add(thNode.InnerText);
                    }
                }

                var tbodyNode = tableNode.ChildNodes.Where(s => s.Name == "tbody").First();
                var trNodeEnum = tbodyNode.ChildNodes.Where(s => s.Name == "tr");

                foreach (var tr in trNodeEnum)
                {
                    var list = new List<string>();

                    var nodes = tr.ChildNodes.Where(s => s.Name == "td" || s.Name == "th");
                    foreach (var td in nodes)
                    {
                        list.Add(WebUtility.HtmlDecode(td.InnerText.Trim()));
                    }

                    valueList.Add(list);
                }
            }
            else
            {
                throw new CpolarException("获取状态失败");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("请求出错：");
            Console.WriteLine("错误信息：{0}", e.Message);
        }

        return result;
    }
}