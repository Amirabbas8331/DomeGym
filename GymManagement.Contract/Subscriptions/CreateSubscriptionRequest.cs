

using GymManagement.Domain.ValueObjects;

namespace GymManagement.Contract.Subscriptions;

public record CreateSubscriptionRequest(SubscriptionId SubscriptionId,string firstname,
        string lastname,SubscriptionType SubscriptionType,Guid AdminId);
