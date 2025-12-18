
using ErrorOr;

namespace GymManagement.Domain.ValueObjects;

public record SubscriptionFirstName
{
    public string FirstName { get; }

    private SubscriptionFirstName(string firstname) => FirstName = firstname;

    public static ErrorOr<SubscriptionFirstName> Create(string value) =>
        ValueObjectFactory.Create(value, v => new SubscriptionFirstName(v));
}
