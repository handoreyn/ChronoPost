using Ardalis.SharedKernel;
using ChronoPost.Core.Enums;

namespace ChronoPost.Core.ValueObjects;

public class UserCredentialValueObject : ValueObject
{
    public string Username { get; init; }
    public string Password { get; init; }

    public UserCredentialValueObject(string username, string password)
    {
        Username = username;
        Password = password;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}