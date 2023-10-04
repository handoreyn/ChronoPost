using Ardalis.SharedKernel;

namespace ChronoPost.UseCases.Users.Queries.FindUserById;

public sealed record FindUserByIdQuery(int UserId) : IQuery<FindUserByIdQueryResponse>;