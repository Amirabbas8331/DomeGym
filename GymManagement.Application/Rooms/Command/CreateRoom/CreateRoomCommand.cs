
using ErrorOr;
using GymManagement.Domain.Rooms;
using MediatR;

namespace GymManagement.Application.Rooms.Command.CreateRoom;
public record CreateRoomCommand(string RoomName,Guid GymId):IRequest<ErrorOr<Room>>;
