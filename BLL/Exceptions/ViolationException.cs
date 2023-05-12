using System.Runtime.Serialization;

namespace BLL.Exceptions;

public class ViolationException : Exception
{
    public ViolationException() { }

    public ViolationException(string message) : base(message) { }

    public ViolationException(string message, Exception innerException) : base(message, innerException)
    {
    }
    protected ViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}