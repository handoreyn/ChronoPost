using Ardalis.SharedKernel;

namespace ChronoPost.UseCases.Users.Queries.GenerateJwtToken;

public sealed record GenerateJwtTokenQuery(string Username, string Password) : IQuery<GenerateJwtTokenQueryResponse>;