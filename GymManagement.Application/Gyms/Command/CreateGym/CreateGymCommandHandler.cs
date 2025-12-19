using ErrorOr;
using FluentValidation;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Gyms.Command.CreateGym;
public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
{
    private readonly ISubscriptionRepository _Subscriptionrepository;
    private readonly IGymRepository _gymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGymCommandHandler(ISubscriptionRepository subscriptionrepository,IGymRepository gymRepository, IUnitOfWork unitOfWork)
    {
        _Subscriptionrepository = subscriptionrepository;
        _gymRepository = gymRepository;
        _unitOfWork = unitOfWork;

    }
    public async Task<ErrorOr<Gym>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
    { 

        var subscription = await _Subscriptionrepository.GetByIdAsync(request.subscriptionid);
        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        var gym = 
            Gym.Create(
            name: request.Name,
            subscriptionId: subscription.Id.Value, 
            maxRooms:subscription.GetMaxRooms());

        var addgymresult = subscription.AddGym(gym);
        

        if (addgymresult.IsError)
        {
            return addgymresult.Errors;
        }
        await _Subscriptionrepository.UpdateAsync(subscription);
        await _gymRepository.AddGymAsync(gym,cancellationToken);
        await _unitOfWork.CommitChangesAsync();
        return gym;
    }
}
