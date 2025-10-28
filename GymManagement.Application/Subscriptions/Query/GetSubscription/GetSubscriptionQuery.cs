using ErrorOr;
using GymManagement.Domain.Subscriptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Query.GetSubscription
{
  public record GetSubscriptionQuery(Guid id):IRequest<ErrorOr<Subscription>>;
}
