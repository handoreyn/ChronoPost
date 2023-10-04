using Ardalis.SharedKernel;
using ChronoPost.Core.Exceptions;
using ChronoPost.Core.Specifications.User;

namespace ChronoPost.UseCases.Users.Queries.FindUserById;

public sealed class FindUserByIdQueryHandler : IQueryHandler<FindUserByIdQuery, FindUserByIdQueryResponse>
{
    private readonly IReadRepository<Core.Aggregates.User> _repository;
    public FindUserByIdQueryHandler(IReadRepository<Core.Aggregates.User> repository)
    {
        _repository = repository;
    }

    public async Task<FindUserByIdQueryResponse> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new UserByIdSpecification(request.UserId);

        var user = await _repository.GetByIdAsync(request.UserId, cancellationToken) ?? throw new UserDoesNotExistException();
        var result = new FindUserByIdQueryResponse(user.Id, user.UserCredentials.Username);

        return result;
    }
}