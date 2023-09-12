using MediatR;

namespace ChronoPost.Core.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}