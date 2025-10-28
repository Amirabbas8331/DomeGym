using Ardalis.SmartEnum;

namespace GymManagement.Domain.Subscriptions;

public class SubscriptionType : SmartEnum<SubscriptionType>
{
    public static readonly SubscriptionType Free = new(nameof(Free), 0, 1);
    public static readonly SubscriptionType Starter = new(nameof(Starter), 1, 1);
    public static readonly SubscriptionType Pro = new(nameof(Pro), 2, 3);

    public int MaxGyms { get; }

    private SubscriptionType(string name, int value, int maxGyms) : base(name, value)
    {
        MaxGyms = maxGyms;
    }
}
