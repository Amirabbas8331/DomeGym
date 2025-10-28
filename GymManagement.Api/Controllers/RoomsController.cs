using GymManagement.Application.Gyms.Command.CreateGym;
using GymManagement.Application.Gyms.Command.DeleteGym;
using GymManagement.Application.Rooms.Command.CreateRoom;
using GymManagement.Application.Rooms.Command.DeleteRoom;
using GymManagement.Contract.Gyms;
using GymManagement.Contract.Rooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers
{
    [Route("gyms/{gymid:guid}/rooms")]
    public class RoomsController : ApiController
    {
        private readonly ISender _sender;

        public RoomsController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomRequest request, Guid gymid)
        {
            var command = new CreateRoomCommand(request.Name, gymid);
            var CreateGymresult = await _sender.Send(command);
            return CreateGymresult.Match(
            room => Created(
                $"rooms/{room.Id}", // todo: add host
                new RoomResponse(room.Id, room.Name)),
            Problem);

        }

        [HttpDelete("{roomid:guid}")]
        public async Task<IActionResult> DeleteRoom(Guid gymId, Guid roomid )
        {
            var command = new DeleteRoomCommand(gymId,roomid);
            var DeleteSubscriptionresult = await _sender.Send(command);
            return DeleteSubscriptionresult.Match(
                _ => NoContent(),
                Problem);

        }
    }
}
