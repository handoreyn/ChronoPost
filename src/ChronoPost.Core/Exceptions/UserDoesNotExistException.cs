using System.Runtime.Serialization;

namespace ChronoPost.Core.Exceptions;

public class UserDoesNotExistException : Exception
{
    public UserDoesNotExistException()
    {
    }

    protected UserDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UserDoesNotExistException(string? message) : base(message)
    {
    }

    public UserDoesNotExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}