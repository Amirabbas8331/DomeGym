using ErrorOr;
using GymManagement.Application.Common.Interfaces;

namespace GymManagement.Application.Gyms.Query.GetGym;
public record GetGymQuery(Guid GymId,Guid subscriptionId):ICommandBase<ErrorOr<Gym>>;

