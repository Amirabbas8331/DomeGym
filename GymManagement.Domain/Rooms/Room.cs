using ErrorOr;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Domain.Rooms;

public class Room
{
    public Guid Id { get; }
    public string Name { get; } = null!;
    public Guid GymId { get; }
    public int MaxDailySessions { get; }
    public Room(
       string name,
       Guid gymId,
       int maxDailySessions,
       Guid? id = null)
    {
        Name = name;
        GymId = gymId;
        MaxDailySessions = maxDailySessions;
        Id = id ?? Guid.NewGuid();
    }
    public static ErrorOr<Room> Create(string name, Guid gymid, int maxdailysession)
    {
        return new Room(name,gymid,maxdailysession);
    }
}
