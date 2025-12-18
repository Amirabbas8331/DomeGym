using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;
namespace GymManagement.Application.Gyms.Command.RemoveTrainer;

public record RemoveTrainerCommand(Guid GymId,Guid TrainerId) :ICommandBase<ErrorOr<Deleted>>;