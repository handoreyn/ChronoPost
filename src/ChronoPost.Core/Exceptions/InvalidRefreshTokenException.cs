using System.Runtime.Serialization;

namespace ChronoPost.Core.Exceptions;

public class InvalidRefreshTokenException : Exception
{
    public InvalidRefreshTokenException()
    {
    }

    protected InvalidRefreshTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidRefreshTokenException(string message) : base(message)
    {
    }

    public InvalidRefreshTokenException(string message, Exception innerException) : base(message, innerException)
    {
    }
}