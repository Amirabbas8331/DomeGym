
using GymManagement.Application.Common.Interfaces;
using GymManagement.Infrastructure.Admins.Persistence;
using GymManagement.Infrastructure.Common.Persistence;
using GymManagement.Infrastructure.Gyms.Persistence;
using GymManagement.Infrastructure.Subscriptions.persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service )
    {
        service.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        service.AddScoped<IUnitOfWork>(serviceProvider=>serviceProvider.GetRequiredService<GymManagementDbContext>());
        service.AddScoped<IGymRepository, GymRepository>();
        service.AddScoped<IAdminRepository, AdminRepository>();

        service.AddDbContext<GymManagementDbContext>(option=>option.UseNpgsql(
           "Host=localhost;Username=Project;Password=@Amir1383;Database=GymManagementDb"));
        return service;
    }
}
