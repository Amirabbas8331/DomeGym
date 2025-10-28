using ErrorOr;
using MediatR;
namespace GymManagement.Application.Gyms.Command.RemoveTrainer;

public record RemoveTrainerCommand(Guid GymId,Guid TrainerId) :IRequest<ErrorOr<Deleted>>;