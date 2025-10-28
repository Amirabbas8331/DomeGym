using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Query.ListGyms;
public class ListGymsQueryHandler : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
{
    private readonly IGymRepository _gymRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;

    public ListGymsQueryHandler(IGymRepository gymRepository,ISubscriptionRepository subscriptionRepository)
    {
        _gymRepository = gymRepository;
        _subscriptionRepository = subscriptionRepository;
    }
    public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery query, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.ExistsAsync(query.subscriptionId);
        if (!await _subscriptionRepository.ExistsAsync(query.subscriptionId))
        {
            return Error.NotFound(description: "subscription not found");
        }
        return await _gymRepository.ListBySubscriptionIdAsync(query.subscriptionId);
    
    }
}
