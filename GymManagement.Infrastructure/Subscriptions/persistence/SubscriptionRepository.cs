using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Subscriptions.persistence;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly GymManagementDbContext _context;
    public SubscriptionRepository(GymManagementDbContext context)
    {
        _context = context;
       
    }
    public async Task AddSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken)
    {
        await _context.Subscriptions.AddAsync(subscription);
        
    }

    public  Task RemoveSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken)
    {
        _context.Subscriptions.Remove(subscription);
        return Task.CompletedTask;
    }

    public async Task<Subscription?> GetByIdAsync(Guid id)
    {
        return await _context.Subscriptions.FindAsync(id);

    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Subscriptions.AsNoTracking().AnyAsync(s=>s.Id.Value==id);
    }

    public async Task<Subscription?> GetByAdminIdAsync(Guid adminId)
    {
        return await _context.Subscriptions.AsNoTracking().FirstOrDefaultAsync(s=>s.AdminId==adminId);
    }

    public async Task<List<Subscription>> ListAsync()
    {
        return await _context.Subscriptions.ToListAsync();
    }

    public  Task UpdateAsync(Subscription subscription)
    {
        _context.Subscriptions.Update(subscription);
        return Task.CompletedTask;
       

    }
}
