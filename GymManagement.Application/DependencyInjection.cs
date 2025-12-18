using GymManagement.Application.Validation;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace GymManagement.Application;

public static class DependencyInjection
{
   
    public static IServiceCollection AddApplication(this IServiceCollection service )
    {
        service.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));

            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        service.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());


        return service;
    }
}
