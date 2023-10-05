namespace ChronoPost.UseCases.Users.Queries.RefreshJwtToken;

public record RefreshJwtTokenQueryResponse(string AccessToken, string RefreshToken);