using CpolarAutoConnect.Core.Entity;

namespace CpolarAutoConnect.Xshell.Entity;

public class XshellSetting : CoreSetting
{
    public string XshellExeLocation { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{base.ToString()}, {nameof(XshellExeLocation)}: {XshellExeLocation}";
    }
}