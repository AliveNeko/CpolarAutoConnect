using CpolarAutoConnect.Core.Util;

var list = await CpolarStatusUtil.GetStatusList();

list.ForEach(s => Console.WriteLine(s));