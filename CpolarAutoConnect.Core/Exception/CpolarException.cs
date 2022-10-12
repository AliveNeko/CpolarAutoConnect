using System.Runtime.Serialization;

namespace CpolarAutoConnect.Core.Exception;

public class CpolarException : System.Exception
{
    public CpolarException()
    {
    }

    protected CpolarException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public CpolarException(string? message) : base(message)
    {
    }

    public CpolarException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}