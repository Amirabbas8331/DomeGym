using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins.Events;
using MediatR;

namespace GymManagement.Application.Gyms.EventsHandler;

public class SubscriptionDeleteEventHandler : INotificationHandler<SubscriptionDeleteEvent>
{
    private readonly IGymRepository gymRepository;
    private readonly IUnitOfWork unitOfWork;

    public SubscriptionDeleteEventHandler(IGymRepository gymRepository,IUnitOfWork unitOfWork)
    {
        this.gymRepository = gymRepository;
        this.unitOfWork = unitOfWork;
    }
    public async Task Handle(SubscriptionDeleteEvent notification, CancellationToken cancellationToken)
    {
        var gyms= await gymRepository.ListBySubscriptionIdAsync(notification.SubscriptionId);
        await gymRepository.RemoveRangeAsync(gyms);
        await unitOfWork.CommitChangesAsync();
    }
}
