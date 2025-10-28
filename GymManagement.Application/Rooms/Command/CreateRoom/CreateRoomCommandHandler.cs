using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Rooms;
using MediatR;

namespace GymManagement.Application.Rooms.Command.CreateRoom;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
{
    private readonly IGymRepository _gymRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubscriptionRepository _subscriptionRepository;

    public CreateRoomCommandHandler(IGymRepository gymRepository, IUnitOfWork unitOfWork, ISubscriptionRepository subscriptionRepository)
    {
        _gymRepository = gymRepository;
        _unitOfWork = unitOfWork;
        _subscriptionRepository = subscriptionRepository;

    }
    public async Task<ErrorOr<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var gym = await _gymRepository.GetByIdAsync(request.GymId);
        if (gym is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }
        var subscription = await _subscriptionRepository.GetByIdAsync(gym.SubscriptionId);
        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }
        var room = new Room(
            request.RoomName,
            request.GymId,
            subscription.GetMaxDailySessions());
        var addGymResult = gym.AddRoom(room);

        if (addGymResult.IsError)
        {
            return addGymResult.Errors;
        }
        await _gymRepository.UpdateGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();
        return room;
    }
}
