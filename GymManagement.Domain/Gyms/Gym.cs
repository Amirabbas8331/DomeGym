using ErrorOr;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Rooms;
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

    public Gym(string name, Guid subscriptionId, int maxRooms, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        SubscriptionId = subscriptionId;
        MaxRooms = maxRooms;
    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        RoomIds.Throw().IfContains(room.Id);

        if (RoomIds.Count >= MaxRooms)
            return GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows;

        RoomIds.Add(room.Id);
        return Result.Success;
    }
    public ErrorOr<Success> RemoveRoom(Guid roomId)
    {
        RoomIds.Throw().IfNotContains(roomId);
        RoomIds.Remove(roomId);
        return Result.Success;
    }
    public bool HasRoom(Guid id)
    {
        return RoomIds.Contains(id);
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
}
