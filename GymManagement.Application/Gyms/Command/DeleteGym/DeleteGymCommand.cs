
using ErrorOr;
using MediatR;

namespace GymManagement.Application.Gyms.Command.DeleteGym;

public record DeleteGymCommand(Guid gymid,Guid subscriptionId):IRequest<ErrorOr<Deleted>>;
