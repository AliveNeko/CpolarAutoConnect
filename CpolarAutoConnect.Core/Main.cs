using CpolarAutoConnect.Core.Entity;
using CpolarAutoConnect.Core.Exception;
using CpolarAutoConnect.Core.Util;

var list = new List<CpolarTunnel>();

try
{
    list = await CpolarStatusUtil.GetStatusList();
}
catch (CpolarException e)
{
    MessageUtil.Alert("错误", e.Message);
}

list.ForEach(s => Console.WriteLine(s));