using Ardalis.Specification;
using ChronoPost.Core.ValueObjects;

namespace ChronoPost.Core.Specifications.User;

public sealed class UserByUserCredentialSpecification : Specification<Aggregates.User>, ISingleResultSpecification<Aggregates.User>
{
    public UserByUserCredentialSpecification(UserCredentialValueObject credentails)
    {
        Query.Where(user => user.UserCredentials.Equals(credentails));
    }
}