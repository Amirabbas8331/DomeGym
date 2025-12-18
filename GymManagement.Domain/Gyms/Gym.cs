using ErrorOr;
using GymManagement.Domain.Gyms;
using Throw;

public class Gym
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Guid SubscriptionId { get; private set; }
    public int MaxRooms { get; private set; }

    public List<Guid> RoomIds { get; private set; } = new();
    public List<Guid> TrainerIds { get; private set; } = new();

    private Gym() { }

    private Gym(string name, Guid subscriptionId, int maxRooms, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        SubscriptionId = subscriptionId;
        MaxRooms = maxRooms;
    }

    public static ErrorOr<Gym> Create(string name, Guid subscriptionId, int maxRooms)
    {
        maxRooms.Throw().IfLessThanOrEqualTo(0);

        return new Gym(name, subscriptionId, maxRooms);
    }

    public ErrorOr<Success> AddRoom(Guid roomId)
    {
        RoomIds.Throw().IfContains(roomId);

        if (RoomIds.Count >= MaxRooms)
            return GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows;

        RoomIds.Add(roomId);
        return Result.Success;
    }

    public ErrorOr<Success> RemoveRoom(Guid roomId)
    {
        RoomIds.Throw().IfNotContains(roomId);

        RoomIds.Remove(roomId);
        return Result.Success;
    }
    public ErrorOr<Success> HasRoom(Guid roomId)
    {
        RoomIds.Throw().IfNotContains(roomId);

        return Result.Success;
    }
    public ErrorOr<Success> AddTrainer(Guid trainerId)
    {
        TrainerIds.Throw().IfContains(trainerId);

        TrainerIds.Add(trainerId);
        return Result.Success;
    }

    public ErrorOr<Success> RemoveTrainer(Guid trainerId)
    {
        TrainerIds.Throw().IfNotContains(trainerId);

        TrainerIds.Remove(trainerId);
        return Result.Success;
    }

    public bool HasTrainer(Guid trainerId) => TrainerIds.Contains(trainerId);
}
