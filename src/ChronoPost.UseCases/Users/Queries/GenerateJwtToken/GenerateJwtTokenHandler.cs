using Ardalis.SharedKernel;
using ChronoPost.Core.Aggregates;
using ChronoPost.Core.Exceptions;
using ChronoPost.Core.Services.Jwt;
using ChronoPost.Core.Specifications.User;

namespace ChronoPost.UseCases.Users.Queries.GenerateJwtToken;

public sealed class GenerateJwtTokenHandler : IQueryHandler<GenerateJwtTokenQuery, GenerateJwtTokenQueryResponse>
{
    private readonly IReadRepository<User> _repository;
    private readonly IJwtService _jwtService;

    public GenerateJwtTokenHandler(IReadRepository<User> repository, IJwtService jwtService)
    {
        _repository = repository;
        _jwtService = jwtService;
    }

    public async Task<GenerateJwtTokenQueryResponse> Handle(GenerateJwtTokenQuery request, CancellationToken cancellationToken)
    {
        var spec = new UserByUserCredentialSpecification(new Core.ValueObjects.UserCredentialValueObject(request.Username, request.Password));
        var user = await _repository.FirstOrDefaultAsync(spec, cancellationToken)
            ?? throw new UserDoesNotExistException("Incorrect username or password! User does not exist");

        var token = _jwtService.GenerateAccessToken(new JwtPayload(user.Id, user.UserCredentials.Username));
        return new GenerateJwtTokenQueryResponse(token.AccessToken, token.RefreshToken);
    }
}