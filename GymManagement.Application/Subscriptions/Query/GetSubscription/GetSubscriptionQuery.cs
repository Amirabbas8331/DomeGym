using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Subscriptions.Query.GetSubscription
{
  public record GetSubscriptionQuery(Guid id):ICommandBase<ErrorOr<Subscription>>;
}
