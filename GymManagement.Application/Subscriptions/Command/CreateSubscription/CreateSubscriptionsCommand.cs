using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.Domain.ValueObjects;
namespace GymManagement.Application.Subscriptions.Command.CreateSubscription;

public record CreateSubscriptionsCommand(SubscriptionId SubscriptionId,string firstname,
        string lastname, Guid AdminId, string SubscriptionType):ICommandBase<ErrorOr<Subscription>>;
