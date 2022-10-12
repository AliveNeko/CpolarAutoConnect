using CpolarAutoConnect.Core.Entity;

namespace CpolarAutoConnect.Xshell.Entity;

public class XshellSetting : CoreSetting
{
    public string XshellExeLocation { get; set; } = null!;
    public string DefaultTunnelName { get; set; } = null!;

    public override string ToString()
    {
        return $"{base.ToString()}, {nameof(XshellExeLocation)}: {XshellExeLocation}";
    }
}