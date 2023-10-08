using ChronoPost.Core.Aggregates.Events;
using MediatR;

namespace ChronoPost.Core.Aggregates.Handlers;

public sealed class UserAuthenticatedEventHandler : INotificationHandler<UserAuthenticatedEvent>
{
    public Task Handle(UserAuthenticatedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}