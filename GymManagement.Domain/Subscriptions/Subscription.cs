
using ErrorOr;
using GymManagement.Domain.Gyms;
using Throw;

namespace GymManagement.Domain.Subscriptions;
public class Subscription
{
    public Guid Id { get; private set; }
    public  SubscriptionType subscriptionType { get; private set; } = null!;

    public Guid AdminId { get; }
    public List<Guid> GymIds { get; private set; } = new();
    public Subscription(
        SubscriptionType SubscriptionType,
        Guid adminId,
        Guid? id = null)
    {
        subscriptionType = SubscriptionType;
        AdminId = adminId;
        Id = id ?? Guid.NewGuid();
    

    }
    public ErrorOr<Success> AddGym(Gym gym)
    {
        if (GymIds.Contains(gym.Id))
        {
            return Error.Validation("GymAlreadyExists", "This gym is already added.");
        }

        if (GymIds.Count >= subscriptionType.MaxGyms)
        {
            return SubscriptionErrors.CannotHaveMoreGymsThanTheSubscriptionAllows;
        }

        GymIds.Add(gym.Id);
        return Result.Success;
    }
    public int GetMaxRooms() => subscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 3,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxDailySessions() => subscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 4,
        nameof(SubscriptionType.Starter) => int.MaxValue,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public bool HasGym(Guid gymid)
    {
        return GymIds.Contains(gymid);
    }

    public void RemoveGym(Guid gymid)
    {
        GymIds.Throw().IfNotContains(gymid);

        GymIds.Remove(gymid);
    }

    private Subscription()
    {
    }
}
 

