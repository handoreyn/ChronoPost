using MediatR;

namespace ChronoPost.Core.Abstractions.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<ICommand> where TCommand : ICommand
{
    
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    
}