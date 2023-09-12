using Ardalis.Specification;

namespace ChronoPost.Core.Specifications.User;

public sealed class UserByIdSpecification : Specification<Aggregates.User>, ISingleResultSpecification<Aggregates.User>
{
    public UserByIdSpecification(int userId)
    {
        Query.Where(u => u.Id == userId);
    }
}