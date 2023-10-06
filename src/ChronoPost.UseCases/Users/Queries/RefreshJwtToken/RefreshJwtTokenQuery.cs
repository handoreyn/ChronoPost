using Ardalis.SharedKernel;

namespace ChronoPost.UseCases.Users.Queries.RefreshJwtToken;

public sealed record RefreshJwtTokenQuery(string RefreshToken, int UserId) : IQuery<RefreshJwtTokenQueryResponse>;