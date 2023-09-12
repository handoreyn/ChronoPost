using Ardalis.SharedKernel;
using ChronoPost.Core.Aggregates;
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
        yield return Username;
        yield return Password;
    }
}