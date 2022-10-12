using System.Diagnostics;
using CpolarAutoConnect.Core.Util;
using CpolarAutoConnect.Xshell.Entity;

var xshellSetting = SettingUtil.GetSetting<XshellSetting>();

if (xshellSetting == null)
{
    Console.WriteLine("找不到xhell设定");
    return;
}

Console.WriteLine(xshellSetting);

Process process = new Process()
{
    StartInfo = new ProcessStartInfo()
    {
        FileName = xshellSetting.XshellExeLocation,
        // https://netsarang.atlassian.net/wiki/spaces/ENSUP/pages/419957436/Xshell+Command+Line+Option
        Arguments = "-url ssh://ip:port",
    }
};

process.Start();

