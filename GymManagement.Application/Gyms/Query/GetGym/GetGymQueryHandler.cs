using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Query.GetGym;
public class GetGymQueryHandler : IRequestHandler<GetGymQuery, ErrorOr<Gym>>
{
    private readonly IGymRepository _gymRepository;
    private readonly ISubscriptionRepository _subscriptionsRepository;

    public GetGymQueryHandler(IGymRepository gymRepository, ISubscriptionRepository subscriptionsRepository)
    {
        _gymRepository = gymRepository;
        _subscriptionsRepository = subscriptionsRepository;

    }
    public async Task<ErrorOr<Gym>> Handle(GetGymQuery request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.ExistsAsync(request.subscriptionId);
        if (!subscription)
        {
            return Error.NotFound(description: "subscription not found");
        }
        var gym = await _gymRepository.GetByIdAsync(request.GymId);
        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }
        return gym;
    }
}
