using ErrorOr;
using MediatR;
using GymManagement.Domain.Gyms;
namespace GymManagement.Application.Gyms.Command.CreateGym;
public record CreateGymCommand(string Name,Guid subscriptionid):IRequest<ErrorOr<Gym>>;
