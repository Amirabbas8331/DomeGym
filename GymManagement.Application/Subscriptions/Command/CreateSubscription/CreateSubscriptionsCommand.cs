using ErrorOr;
using GymManagement.Domain.Subscriptions;
using MediatR;
namespace GymManagement.Application.Subscriptions.Command.CreateSubscription;

public record CreateSubscriptionsCommand(string firstname,
        string lastname, Guid AdminId, string SubscriptionType):IRequest<ErrorOr<Subscription>>;
