

namespace GymManagement.Domain.ValueObjects;

public abstract class IdBase<T>(T Value)
{
    public override string ToString() => Value?.ToString() ?? string.Empty;
}
