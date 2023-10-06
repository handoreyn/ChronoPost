namespace ChronoPost.UseCases.Users.Queries.RefreshJwtToken;

public sealed record RefreshJwtTokenQueryResponse(string AccessToken, string RefreshToken);