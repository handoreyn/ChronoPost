using ChronoPost.Core.Aggregates.Events;
using ChronoPost.Core.Extensions.CacheExtensions;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace ChronoPost.Core.Aggregates.Handlers;

public sealed class UserAuthenticatedEventHandler : INotificationHandler<UserAuthenticatedEvent>
{
    private readonly IDistributedCache _cache;

    public UserAuthenticatedEventHandler(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Handle(UserAuthenticatedEvent notification, CancellationToken cancellationToken)
    {
        await _cache.SetCacheAsync(notification.RefreshToken, notification.Payload, notification.ExpirationInMinutes, cancellationToken);
    }
}