using Ardalis.SharedKernel;

namespace ChronoPost.UseCases.Users.Queries.RefreshJwtToken;

public record RefreshJwtTokenQuery(string RefreshToken, int UserId) : IQuery<RefreshJwtTokenQueryResponse>;