using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Gyms.Command.AddTrainer;

public class AddTrainerCommandHandler : IRequestHandler<AddTrainerCommand, ErrorOr<Success>>
{
    private readonly IGymRepository _gymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddTrainerCommandHandler(IGymRepository gymRepository, IUnitOfWork unitOfWork)
    {
        _gymRepository = gymRepository;
        _unitOfWork = unitOfWork;

    }
    public async Task<ErrorOr<Success>> Handle(AddTrainerCommand request, CancellationToken cancellationToken)
    {
        var gym = await _gymRepository.GetByIdAsync(request.Gymid);
        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }
        var addTrainerResult = gym.AddTrainer(request.TrainerId);

        if (addTrainerResult.IsError)
        {
            return addTrainerResult.Errors;
        }
        await _gymRepository.UpdateGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();
        return Result.Success; 

    }
}
