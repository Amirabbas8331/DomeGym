using ErrorOr;
using MediatR;
using GymManagement.Domain.Gyms;
using GymManagement.Application.Common.Interfaces;
namespace GymManagement.Application.Gyms.Command.CreateGym;
public record CreateGymCommand(string Name,Guid subscriptionid):ICommandBase<ErrorOr<Gym>>;
