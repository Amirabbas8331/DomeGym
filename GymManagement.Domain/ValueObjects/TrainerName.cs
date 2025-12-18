

using ErrorOr;

namespace GymManagement.Domain.ValueObjects;

public class TrainerName
{
    public string Name { get; }

    private TrainerName(string name) => Name = name;

    public static ErrorOr<TrainerName> Create(string value) =>
        ValueObjectFactory.Create(value, v => new TrainerName(v));
}
