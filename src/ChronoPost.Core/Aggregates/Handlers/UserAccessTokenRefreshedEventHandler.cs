using ChronoPost.Core.Aggregates.Events;
using ChronoPost.Core.Extensions.CacheExtensions;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace ChronoPost.Core.Aggregates.Handlers;

public sealed class UserAccessTokenRefreshedEventHandler : INotificationHandler<UserAccessTokenRefreshedEvent>
{
    private readonly IDistributedCache _cache;

    public UserAccessTokenRefreshedEventHandler(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Handle(UserAccessTokenRefreshedEvent notification, CancellationToken cancellationToken)
    {
        var removeTask = _cache.RemoveAsync(notification.PreviousRefreshToken, cancellationToken);
        var setTask = _cache.SetCacheAsync(notification.RefreshToken, notification.Payload, notification.ExpirationInMinutes, cancellationToken);

        await Task.WhenAll(removeTask, setTask);
    }
}
