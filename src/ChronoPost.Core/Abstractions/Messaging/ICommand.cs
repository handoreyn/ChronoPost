using MediatR;

namespace ChronoPost.Core.Abstractions.Messaging;

public interface ICommand : IRequest
{
    
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
    
}