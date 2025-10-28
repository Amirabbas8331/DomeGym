using ErrorOr;
using MediatR;


namespace GymManagement.Application.Gyms.Command.AddTrainer;

public record AddTrainerCommand(Guid Gymid,Guid TrainerId) :IRequest<ErrorOr<Success>>;
