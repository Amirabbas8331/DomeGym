
namespace GymManagement.Domain.Abstrctions;

public abstract class AggregateRoot<TId> : EntityBase<TId>, IAggregateRoot
    where TId : notnull
{


    public AggregateRoot(TId id)
        : base(id) { }

}
