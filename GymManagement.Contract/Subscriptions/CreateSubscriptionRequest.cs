

namespace GymManagement.Contract.Subscriptions;

public record CreateSubscriptionRequest(string firstname,
        string lastname,SubscriptionType SubscriptionType,Guid AdminId);
