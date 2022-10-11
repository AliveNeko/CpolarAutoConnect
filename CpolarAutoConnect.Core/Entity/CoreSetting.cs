using System.Diagnostics.CodeAnalysis;

namespace CpolarAutoConnect.Core.Entity;

public class CoreSetting
{
    public string LoginName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{nameof(LoginName)}: {LoginName}, {nameof(Password)}: {Password}";
    }
}