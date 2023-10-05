using Ardalis.SharedKernel;

namespace ChronoPost.UseCases.Users.Queries.RefreshJwtToken;

public class RefreshTokenQueryHandler : IQueryHandler<RefreshJwtTokenQuery, RefreshJwtTokenQueryResponse>
{
    public Task<RefreshJwtTokenQueryResponse> Handle(RefreshJwtTokenQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}