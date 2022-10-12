using System.Diagnostics.CodeAnalysis;

namespace CpolarAutoConnect.Core.Entity;

public class CoreSetting
{
    public string LoginName { get; set; } = null!;
    public string Password { get; set; } = null!;

    public override string ToString()
    {
        return $"{nameof(LoginName)}: {LoginName}, {nameof(Password)}: {Password}";
    }
}