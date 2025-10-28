
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Common.Interfaces;
public interface IGymRepository
{
    Task AddGymAsync(Gym gym, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id);
    Task<Gym?> GetByIdAsync(Guid id);
    Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId);
    Task RemoveGymAsync(Gym gym, CancellationToken cancellationToken);
    Task UpdateGymAsync(Gym gym);
    Task RemoveRangeAsync(List<Gym> gyms);
}
