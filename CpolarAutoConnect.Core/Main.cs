using System.Net;
using CpolarAutoConnect.Core.Entity;
using CpolarAutoConnect.Core.Util;
using HtmlAgilityPack;

var pair = await CpolarStatusUtil.GetStatus();

Console.WriteLine(string.Join("|", pair.Item1));
foreach (var list in pair.Item2)
{
    Console.WriteLine(string.Join("|", list));
}