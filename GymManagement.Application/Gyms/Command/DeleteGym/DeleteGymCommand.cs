
using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Gyms.Command.DeleteGym;

public record DeleteGymCommand(Guid gymid,Guid subscriptionId):ICommandBase<ErrorOr<Deleted>>;
