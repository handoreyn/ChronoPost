using Ardalis.Specification;

namespace ChronoPost.Core.Specifications.User;

public sealed class UserByUserNameOrEmailSpecification : Specification<Aggregates.User>, ISingleResultSpecification<Aggregates.User>
{
    public UserByUserNameOrEmailSpecification(string username, string email)
    {
        Query.Where(o => o.UserCredentials.Username.Equals(username) && o.Email.Equals(email));
    }
}