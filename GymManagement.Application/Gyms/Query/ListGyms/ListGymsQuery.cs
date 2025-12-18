using ErrorOr;
using GymManagement.Application.Common.Interfaces;

namespace GymManagement.Application.Gyms.Query.ListGyms;
public record ListGymsQuery(Guid subscriptionId):ICommandBase<ErrorOr<List<Gym>>>;

