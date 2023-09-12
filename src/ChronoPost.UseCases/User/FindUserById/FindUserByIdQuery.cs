using Ardalis.SharedKernel;

namespace ChronoPost.UseCases.User.FindUserById;

public record FindUserByIdQuery(int userId) : IQuery<FindUserByIdQueryResponse>;