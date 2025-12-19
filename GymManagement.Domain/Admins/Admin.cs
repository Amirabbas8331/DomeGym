
using ErrorOr;
using GymManagement.Domain.Admins.Events;
using GymManagement.Domain.Common;
using GymManagement.Domain.Subscriptions;
using Throw;

namespace GymManagement.Domain.Admins;

public class Admin:Entity
{
    public Guid UserId { get; }
    public Guid? SubscriptionId { get;private set; }
    public Admin(Guid userId,
        Guid? subscriptionId = null,
        Guid? id = null)
        :base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }
    private Admin() { }

    public ErrorOr<Success> SetSubscription(Subscription subscription)
    {
        SubscriptionId.HasValue.Throw().IfTrue();

        SubscriptionId = subscription.Id.Value;
        return Result.Success;
    }

    public ErrorOr<Success> DeleteSubscription(Guid subscriptionId)
    {
        SubscriptionId.ThrowIfNull().IfNotEquals(subscriptionId);
        SubscriptionId = null;
        _domainEvents.Add(new SubscriptionDeleteEvent(subscriptionId));
        return Result.Success;
    }
}
