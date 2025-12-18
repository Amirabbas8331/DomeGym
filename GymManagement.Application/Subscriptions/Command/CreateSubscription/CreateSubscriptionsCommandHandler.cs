using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.Domain.Validations;
using MediatR;

namespace GymManagement.Application.Subscriptions.Command.CreateSubscription;

public class CreateSubscriptionsCommandHandler : IRequestHandler<CreateSubscriptionsCommand, ErrorOr<Subscription>>
{
    private readonly ISubscriptionRepository _subscriptionsRepository;
    private readonly IAdminRepository _adminsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionsCommandHandler(ISubscriptionRepository subscriptionsRepository, IUnitOfWork unitOfWork, IAdminRepository adminsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _unitOfWork = unitOfWork;
        _adminsRepository = adminsRepository;
    }

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionsCommand request, CancellationToken cancellationToken)
    {
        var admin = await _adminsRepository.GetByIdAsync(request.AdminId);

        if (admin is null)
        {
            return Error.NotFound(description: "Admin not found");
        }
        if (!SubscriptionType.TryFromName(request.SubscriptionType, out var subscriptionType))
        {
            return Error.Validation(description: "Invalid subscription type");
        }

        var subscription = Subscription.Create(request.SubscriptionId,request.firstname,request.lastname ,subscriptionType,adminId: request.AdminId);

        if (admin.SubscriptionId is not null)
        {
            return Error.Conflict(description: "Admin already has an active subscription");
        }

        admin.SetSubscription(subscription.Value);

        await _subscriptionsRepository.AddSubscriptionAsync(subscription.Value, cancellationToken);
        await _adminsRepository.UpdateAsync(admin);
        await _unitOfWork.CommitChangesAsync();

        return subscription;
    }

}

