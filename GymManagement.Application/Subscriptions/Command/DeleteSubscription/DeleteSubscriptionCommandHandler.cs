

using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Command.DeleteSubscription;

public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
{
    private readonly IAdminRepository _adminsRepository;
    private readonly ISubscriptionRepository _subscriptionsRepository;

    private readonly IUnitOfWork _unitOfWork;

    public DeleteSubscriptionCommandHandler(
        IAdminRepository adminsRepository,
        ISubscriptionRepository subscriptionsRepository,
        IUnitOfWork unitOfWork)
    {
        _adminsRepository = adminsRepository;
        _subscriptionsRepository = subscriptionsRepository;
        _unitOfWork = unitOfWork;
 
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        var admin = await _adminsRepository.GetByIdAsync(subscription.AdminId);

        if (admin is null)
        {
            return Error.Unexpected(description: "Admin not found");
        }

        admin.DeleteSubscription(command.SubscriptionId);

        await _adminsRepository.UpdateAsync(admin);
 
        return Result.Deleted;
    }
}
