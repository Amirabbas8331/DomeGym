using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins.Events;
using MediatR;


namespace GymManagement.Application.Subscriptions.EventsHandler;

public class SubscriptionDeleteEventHandler : INotificationHandler<SubscriptionDeleteEvent>
{
    private readonly ISubscriptionRepository subscriptionRepository;
    private readonly IUnitOfWork unitOfWork;

    public SubscriptionDeleteEventHandler(ISubscriptionRepository subscriptionRepository,IUnitOfWork unitOfWork)
    {
        this.subscriptionRepository = subscriptionRepository;
        this.unitOfWork = unitOfWork;
    }
    public async Task Handle(SubscriptionDeleteEvent notification, CancellationToken cancellationToken)
    {
        var subscription = await subscriptionRepository.GetByIdAsync(notification.SubscriptionId)
            ??throw new InvalidOperationException();
         await subscriptionRepository.RemoveSubscriptionAsync(subscription, cancellationToken);
        await unitOfWork.CommitChangesAsync();
    }
}
