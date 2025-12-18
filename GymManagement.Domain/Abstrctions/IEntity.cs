

namespace GymManagement.Domain.Abstrctions;

public interface IEntity { }

public interface IEntity<out TId> : IEntity
    where TId : notnull
{
    TId Id { get; }
}
