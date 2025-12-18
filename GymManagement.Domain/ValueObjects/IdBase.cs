

namespace GymManagement.Domain.ValueObjects;

public abstract record IdBase<T>(T Value)
{
    public override string ToString() => Value?.ToString() ?? string.Empty;
}
