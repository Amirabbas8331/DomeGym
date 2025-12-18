using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace GymManagement.Infrastructure.Gyms.Persistence;
public class GymRepository : IGymRepository
{
    private readonly GymManagementDbContext _context;
    public GymRepository(GymManagementDbContext context)
    {
        _context = context;

    }
    public async Task AddGymAsync(ErrorOr<Gym> gym, CancellationToken cancellationToken)
    {
        await _context.Gyms.AddAsync(gym.Value, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Gyms.AsNoTracking().AnyAsync(gym => gym.Id == id);
    }

    public async Task<Gym?> GetByIdAsync(Guid id)
    {
       return await _context.Gyms.FindAsync(id);
    }

    public async Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId)
    {
        return await _context.Gyms.Where(g=>g.SubscriptionId==subscriptionId).ToListAsync();
    }

    public  Task RemoveGymAsync(Gym gym, CancellationToken cancellationToken)
    {
         _context.Gyms.Remove(gym);
        return Task.CompletedTask;
    }

    public Task RemoveRangeAsync(List<Gym> gyms)
    {
        _context.Gyms.RemoveRange(gyms);
        return Task.CompletedTask;
    }

    public Task UpdateGymAsync(Gym gym)
    {
        _context.Gyms.Update(gym);
        return Task.CompletedTask;
    }
}
