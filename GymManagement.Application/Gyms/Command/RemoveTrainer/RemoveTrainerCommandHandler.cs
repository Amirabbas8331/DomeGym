using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Gyms.Command.RemoveTrainer;

public class RemoveTrainerCommandHandler : IRequestHandler<RemoveTrainerCommand, ErrorOr<Deleted>>
{
    private readonly IGymRepository _gymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveTrainerCommandHandler(IGymRepository gymRepository, IUnitOfWork unitOfWork)
    {
        _gymRepository = gymRepository;
        _unitOfWork = unitOfWork;

    }
    public async Task<ErrorOr<Deleted>> Handle(RemoveTrainerCommand request, CancellationToken cancellationToken)
    {
        var gym = await _gymRepository.GetByIdAsync(request.GymId);
        if (gym is null)
        {
            return Error.NotFound(description: "Gym does not fucking found found");
        }
        var removeTrainer = gym.RemoveTrainer(request.TrainerId);

        if (removeTrainer.IsError)
        {
            return removeTrainer.Errors;
        }
        await _gymRepository.UpdateGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();
        return Result.Deleted;
    }
}
