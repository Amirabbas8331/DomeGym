using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Gyms.Command.DeleteGym;

public class DeleteGymCommandHandler : IRequestHandler<DeleteGymCommand, ErrorOr<Deleted>>
{
    private readonly IGymRepository  _gymRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGymCommandHandler(IGymRepository gymRepository,ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
    {
        _gymRepository = gymRepository;
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;

    }
    public async Task<ErrorOr<Deleted>> Handle(DeleteGymCommand request, CancellationToken cancellationToken)
    {
        var gym =await _gymRepository.GetByIdAsync(request.gymid);

        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }

        var subscription = await _subscriptionRepository.GetByIdAsync(request.subscriptionId);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }
        if(!subscription.HasGym(request.gymid))
        {
            return Error.Unexpected(description: "Gym has not found");
        }
        subscription.RemoveGym(request.gymid);

        await _subscriptionRepository.UpdateAsync(subscription);
        await _gymRepository.RemoveGymAsync(gym,cancellationToken);
        await _unitOfWork.CommitChangesAsync();
        return Result.Deleted;


    }
}
