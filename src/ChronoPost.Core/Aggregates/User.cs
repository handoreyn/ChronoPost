using Ardalis.SharedKernel;
using ChronoPost.Core.Enums;
using ChronoPost.Core.ValueObjects;

namespace ChronoPost.Core.Aggregates;

public sealed class User : EntityBase, IAggregateRoot
{
    public UserCredentialValueObject UserCredentials { get; set; }
    public string Email { get; set; }
    public StatusType Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User(string username, string password, string email, StatusType status = StatusType.Passive)
    {
        Email = email;
        Status = status;
        UserCredentials = new UserCredentialValueObject(username, password);
    }
}