using ErrorOr;
using GymManagement.Application.Subscriptions.Command.CreateSubscription;
using GymManagement.Domain.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Interfaces;

public interface ISubscriptionRepository
{
    Task AddSubscriptionAsync(Subscription subscription,CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id);
    Task<Subscription?> GetByAdminIdAsync(Guid adminId);
    Task<Subscription?> GetByIdAsync(Guid id);
    Task<List<Subscription>> ListAsync();
    Task RemoveSubscriptionAsync(Subscription subscription,CancellationToken cancellationToken);
    Task UpdateAsync(Subscription subscription);


}
