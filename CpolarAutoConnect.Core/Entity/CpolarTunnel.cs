namespace CpolarAutoConnect.Core.Entity;

public class CpolarTunnel
{
    /// <summary>
    /// 隧道名称
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// URL
    /// </summary>
    public string Url { get; set; } = null!;
    /// <summary>
    /// 客户IP
    /// </summary>
    public string IP { get; set; } = null!;
    /// <summary>
    /// 地区
    /// </summary>
    public string Region { get; set; } = null!;
    /// <summary>
    /// 创建时间
    /// </summary>
    public string CreateTime { get; set; } = null!;

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Url)}: {Url}, {nameof(IP)}: {IP}, {nameof(Region)}: {Region}, {nameof(CreateTime)}: {CreateTime}";
    }
}