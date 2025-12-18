
using ErrorOr;
using GymManagement.Domain.Abstrctions;
using GymManagement.Domain.ValueObjects;
using Throw;

namespace GymManagement.Domain.Subscriptions;
public  class Subscription:AggregateRoot<SubscriptionId>
{
    public SubscriptionFirstName FirstName { get; private set; }
    public SubscriptionLastName LastName { get; private set; }
    public SubscriptionType subscriptionType { get; private set; } = null!;

    public Guid AdminId { get; }
    public List<Guid> GymIds { get; private set; } = new();
    private Subscription()
        : base(default!) { }
    public Subscription(
          SubscriptionId SubscriptionId,
        SubscriptionType SubscriptionType,
        Guid adminId,
         SubscriptionFirstName firstname,
        SubscriptionLastName lastname
      
        ):base(SubscriptionId)
    {
        FirstName= firstname;
        LastName= lastname;
        subscriptionType = SubscriptionType;
        AdminId = adminId;

    }
    public static ErrorOr<Subscription> Create(SubscriptionId SubscriptionId, string firstname,
        string lastname, SubscriptionType subscriptionType,Guid adminId)
    {
        return new Subscription(SubscriptionId, subscriptionType, adminId, SubscriptionFirstName.Create(firstname).Value, SubscriptionLastName.Create(lastname).Value);
    }
    public ErrorOr<Success> AddGym(ErrorOr<Gym> gym)
    {
        if (GymIds.Contains(gym.Value.Id))
        {
            return Error.Validation("GymAlreadyExists", "This gym is already added.");
        }

        if (GymIds.Count >= subscriptionType.MaxGyms)
        {
            return SubscriptionErrors.CannotHaveMoreGymsThanTheSubscriptionAllows;
        }

        GymIds.Add(gym.Value.Id);
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

}
 

