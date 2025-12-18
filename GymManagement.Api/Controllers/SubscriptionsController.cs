using GymManagement.Application.Subscriptions.Command.CreateSubscription;
using GymManagement.Application.Subscriptions.Command.DeleteSubscription;
using GymManagement.Application.Subscriptions.Query.GetSubscription;
using GymManagement.Contract.Subscriptions;
using MediatR;
using DomainSubscriptionType = GymManagement.Domain.Subscriptions.SubscriptionType;
using Microsoft.AspNetCore.Mvc;
namespace GymManagement.Api.Controllers
{
    [Route("[controller]")]
    public class SubscriptionsController : ApiController
    {
        private readonly ISender _mediator;

        public SubscriptionsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
        {

            var command = new CreateSubscriptionsCommand(request.firstname,request.lastname,request.AdminId,request.SubscriptionType.ToString());

            var createSubscriptionResult = await _mediator.Send(command);

            return createSubscriptionResult.Match(
                subscription => CreatedAtAction(
                    nameof(GetSubscription),
                    new { subscriptionId = subscription.Id },
                    new SubscriptionResponse(
                        subscription.Id,
                        ToDto(subscription.subscriptionType))),
                Problem);
        }

        [HttpGet("{subscriptionId:guid}")]
        public async Task<IActionResult> GetSubscription(Guid subscriptionId)
        {
            var query = new GetSubscriptionQuery(subscriptionId);

            var getSubscriptionsResult = await _mediator.Send(query);

            return getSubscriptionsResult.Match(
                subscription => Ok(new SubscriptionResponse(
                    subscription.Id,
                    ToDto(subscription.subscriptionType))),
                Problem);
        }

        [HttpDelete("{subscriptionId:guid}")]
        public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
        {
            var command = new DeleteSubscriptionCommand(subscriptionId);

            var createSubscriptionResult = await _mediator.Send(command);

            return createSubscriptionResult.Match(
                _ => NoContent(),
                Problem);
        }

        private static SubscriptionType ToDto(DomainSubscriptionType subscriptionType)
        {
            return subscriptionType.Name switch
            {
                nameof(DomainSubscriptionType.Free) => SubscriptionType.Free,
                nameof(DomainSubscriptionType.Starter) => SubscriptionType.Starter,
                nameof(DomainSubscriptionType.Pro) => SubscriptionType.Pro,
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
