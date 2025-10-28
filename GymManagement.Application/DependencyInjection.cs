using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GymManagement.Application;

public static class DependencyInjection
{
   
    public static IServiceCollection AddApplication(this IServiceCollection service )
    {
        service.AddMediatR(option => option.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));
        
        return service;
    }
}
