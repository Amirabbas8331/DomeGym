

namespace GymManagement.Domain.Abstrctions;

public abstract class EntityBase<TId> : IEntity<TId>
    where TId : notnull
{
    public TId Id { get; protected set; }

    protected EntityBase(TId id) => Id = id;

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        var entity = (EntityBase<TId>)obj;

        return Id.Equals(entity.Id);
    }

    public override int GetHashCode() => Id.GetHashCode() ^ 31;

}
