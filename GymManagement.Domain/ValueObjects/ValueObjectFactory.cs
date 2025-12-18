using ErrorOr;

namespace GymManagement.Domain.ValueObjects;

public class ValueObjectFactory
{
    protected ValueObjectFactory() { }

    public static ErrorOr<TVO> Create<TVO, T>(
        T value,
        Func<T, TVO> creator
    )
    {
        return creator(value);
    }
}
