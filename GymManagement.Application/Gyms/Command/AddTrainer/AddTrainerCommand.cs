using ErrorOr;
using GymManagement.Application.Common.Interfaces;

namespace GymManagement.Application.Gyms.Command.AddTrainer;

public record AddTrainerCommand(Guid Gymid,Guid TrainerId) :ICommandBase<ErrorOr<Success>>;
