using Ardalis.SharedKernel;

namespace ChronoPost.UseCases.User.FindUserById;

public sealed record FindUserByIdQuery(int UserId) : IQuery<FindUserByIdQueryResponse>;