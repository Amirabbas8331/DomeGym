using ErrorOr;


namespace GymManagement.Domain.ValueObjects;

public class SubscriptionId : IdBase<Guid>
{
    public SubscriptionId(Guid Value) : base(Value)
    {
    }
    public static ErrorOr<SubscriptionId> Create(Guid value) =>
        new SubscriptionId(value);
}
