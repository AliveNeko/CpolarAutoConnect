using CpolarAutoConnect.Core.Entity;
using CpolarAutoConnect.Core.Util;

var coreSetting = SettingUtil.GetSetting<CoreSetting>();

Console.WriteLine(coreSetting);

HttpClient httpClient = new HttpClient();

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