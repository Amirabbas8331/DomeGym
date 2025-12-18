using ErrorOr;


namespace GymManagement.Domain.ValueObjects;

public record SubscriptionLastName
{
    public string LastName { get; }

    private SubscriptionLastName(string lastname) => LastName = lastname;

    public static ErrorOr<SubscriptionLastName> Create(string value) =>
        ValueObjectFactory.Create(value, v => new SubscriptionLastName(v));
}
