using System.Net;
using CpolarAutoConnect.Core.Entity;
using CpolarAutoConnect.Core.Util;

var coreSetting = SettingUtil.GetSetting<CoreSetting>();

Console.WriteLine(coreSetting);

HttpClientHandler httpClientHandler = new HttpClientHandler
{
    AllowAutoRedirect = false,
    UseCookies = true,
};

const string CookieRootDomain = ".cpolar.com";
const string CookieDashboardDomain = ".dashboard.cpolar.com";
const string CookiePath = "/";

httpClientHandler.CookieContainer.Add(new CookieCollection()
{
    new Cookie()
    {
        Domain = CookieRootDomain,
        Path = CookiePath,
        Name = "_ga",
        Value = "GA1.2.2069977613.1665490152",
    },
    new Cookie()
    {
        Domain = CookieRootDomain,
        Path = CookiePath,
        Name = "_gid",
        Value = "GA1.2.1409181498.1665490152",
    },
    new Cookie()
    {
        Domain = CookieRootDomain,
        Path = CookiePath,
        Name = "_gat_gtag_UA_128397857_1",
        Value = "1",
    },
    new Cookie()
    {
        Domain = CookieRootDomain,
        Path = CookiePath,
        Name = "_gat_gtag_UA_128397857_1",
        Value = "1",
    },
    new Cookie()
    {
        Domain = CookieRootDomain,
        Path = CookiePath,
        Name = "Hm_lpvt_0838dad5461d14f63bdf207a43a54c29",
        Value = "1665501394",
    },
    new Cookie()
    {
        Domain = CookieRootDomain,
        Path = CookiePath,
        Name = "Hm_lvt_0838dad5461d14f63bdf207a43a54c29",
        Value = "1665466572",
    },
    new Cookie()
    {
        Domain = CookieDashboardDomain,
        Path = CookiePath,
        Name = "session",
        Value = "bfdaf1c8-b70c-4ab5-bedb-6b88155a1763",
    },
});

HttpClient httpClient = new HttpClient(httpClientHandler);


try
{
    HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("https://dashboard.cpolar.com/status");
    Console.WriteLine(httpResponseMessage);
}
catch (HttpRequestException e)
{
    Console.WriteLine("\nException Caught!");
    Console.WriteLine("Message :{0} ", e.Message);
}