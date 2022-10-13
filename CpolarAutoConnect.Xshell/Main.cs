using System.Diagnostics;
using CpolarAutoConnect.Core.Entity;
using CpolarAutoConnect.Core.Util;
using CpolarAutoConnect.Xshell.Entity;

var xshellSetting = SettingUtil.GetSetting<XshellSetting>();

if (xshellSetting == null)
{
    MessageUtil.Alert("错误", "找不到xhell设定");
    return;
}

var statusList = await CpolarStatusUtil.GetStatusList();

CpolarTunnel? cpolarTunnel = statusList.Where(s => s.Name == xshellSetting.DefaultTunnelName).SingleOrDefault();

if (cpolarTunnel == null)
{
    MessageUtil.Alert("错误", $"找不到名为「{xshellSetting.DefaultTunnelName}」的隧道");
    return;
}

Uri uri = new Uri(cpolarTunnel.Url);

Process process = new Process()
{
    StartInfo = new ProcessStartInfo()
    {
        FileName = xshellSetting.XshellExeLocation,
        // https://netsarang.atlassian.net/wiki/spaces/ENSUP/pages/419957436/Xshell+Command+Line+Option
        Arguments = $"-url ssh://{uri.Host}:{uri.Port}",
    }
};

process.Start();