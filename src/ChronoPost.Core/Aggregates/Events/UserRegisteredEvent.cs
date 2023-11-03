using Ardalis.SharedKernel;

namespace ChronoPost.Core.Aggregates.Events;

public sealed class UserRegisteredEvent : DomainEventBase
{
    public string EmailAddress { get; init; }
    public string Username { get; init; }

    public UserRegisteredEvent(string emailAddress, string username)
    {
        EmailAddress = emailAddress;
        Username = username;
    }
}
