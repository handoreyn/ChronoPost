using Ardalis.SharedKernel;
using ChronoPost.Core.Abstractions;

namespace ChronoPost.UseCases.User.FindUserById;

public sealed class FindUserByIdQueryHandler : IQueryHandler<FindUserByIdQuery, FindUserByIdQueryResponse>
{
    private IUserReadRepository _repository;

    public FindUserByIdQueryHandler(IUserReadRepository repository)
    {
        _repository = repository;
    }

    public Task<FindUserByIdQueryResponse> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}