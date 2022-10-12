using CpolarAutoConnect.Core.Util;

var pair = await CpolarStatusUtil.GetStatus();

Console.WriteLine(string.Join("|", pair.Item1));
foreach (var list in pair.Item2)
{
    Console.WriteLine(string.Join("|", list));
}