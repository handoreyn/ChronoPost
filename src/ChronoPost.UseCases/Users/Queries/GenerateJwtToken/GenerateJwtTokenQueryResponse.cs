namespace ChronoPost.UseCases.Users.Queries.GenerateJwtToken;

public sealed record GenerateJwtTokenQueryResponse(string AccessToken, string RefreshToken);