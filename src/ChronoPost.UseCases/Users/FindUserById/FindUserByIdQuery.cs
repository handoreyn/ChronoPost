using Ardalis.SharedKernel;

namespace ChronoPost.UseCases.Users.FindUserById;

public sealed record FindUserByIdQuery(int UserId) : IQuery<FindUserByIdQueryResponse>;