using CpolarAutoConnect.Core.Entity;
using Newtonsoft.Json;

namespace CpolarAutoConnect.Core.Util;

public static class SettingUtil
{
    public static T? GetSetting<T>() where T : CoreSetting
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(@"./setting.json5"));
        }
        catch (FileNotFoundException)
        {
            return null;
        }
    }
}