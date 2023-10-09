using Ardalis.SharedKernel;

namespace ChronoPost.UseCases.Users.Queries.RefreshJwtToken;

public sealed record RefreshJwtTokenQuery(string RefreshToken) : IQuery<RefreshJwtTokenQueryResponse>;