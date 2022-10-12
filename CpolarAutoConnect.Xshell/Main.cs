using CpolarAutoConnect.Core.Util;
using CpolarAutoConnect.Xshell.Entity;

// var pair = await CpolarStatusUtil.GetStatus();

var xshellSetting = SettingUtil.GetSetting<XshellSetting>();

Console.WriteLine(xshellSetting);
