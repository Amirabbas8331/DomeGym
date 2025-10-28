using GymManagement.Application.Gyms.Command.CreateGym;
using GymManagement.Contract.Gyms;
using GymManagement.Application.Gyms.Query.GetGym;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Application.Gyms.Command.DeleteGym;
using GymManagement.Application.Gyms.Query.ListGyms;
using GymManagement.Application.Gyms.Command.AddTrainer;
using GymManagement.Application.Gyms.Command.RemoveTrainer;

namespace GymManagement.Api.Controllers
{
    [Route("subscriptions/{subscriptionId:guid}/gyms")]
    public class GymsController : ApiController
    {
        private readonly ISender _sender;

        public GymsController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost]
        public async Task<IActionResult> CreateGym(CreateGymRequest request, Guid subscriptionId)
        {
            var command = new CreateGymCommand(request.Name, subscriptionId);
            var CreateGymresult = await _sender.Send(command);
            return CreateGymresult.Match(
            gym => CreatedAtAction(
                nameof(GetGym),
                new { subscriptionId, Gymid = gym.Id },
                new GymResponse(gym.Id, request.Name)
                ),
            Problem);

        }
        
        [HttpDelete("{gymId:guid}")]
        public async Task<IActionResult> DeleteGym(Guid GymId,Guid subscriptionId)
        {
            var command = new DeleteGymCommand(GymId,subscriptionId);
            var DeleteSubscriptionresult = await _sender.Send(command);
            return DeleteSubscriptionresult.Match(
                _ => NoContent(),
                Problem);

        }
        [HttpGet("{gymId:guid}")]
        public async Task<IActionResult> GetGym(Guid gymId,Guid subscriptionId)
        {
            var query = new GetGymQuery(gymId, subscriptionId);
            var getSubscriptionsResult = await _sender.Send(query);
            return getSubscriptionsResult.Match(
           gym => Ok(new GymResponse(gym.Id, gym.Name)),
           Problem);
        }

        [HttpGet]
        public async Task<IActionResult> ListGyms(Guid subscriptionId)
        {
            var query = new ListGymsQuery(subscriptionId);
            var ListGymsresult = await _sender.Send(query);
            return ListGymsresult.Match(
            gyms => Ok(gyms.ConvertAll(gym => new GymResponse(gym.Id, gym.Name))),
             Problem);

        }
        [HttpPost("{gymId:guid}/trainers")]
        public async Task<IActionResult> AddTrainer(AddTrainerRequest request, Guid subscriptionId, Guid gymId)
        {
            var command = new AddTrainerCommand(gymId, request.trainerId);
            var AddTrainerresult = await _sender.Send(command);
            return AddTrainerresult.Match(
             success => Ok(),
             Problem);


        }
        [HttpDelete("{gymId:guid}/trainers")]
        public async Task<IActionResult> DeleteTrainer(Guid gymId,Guid TrainerId)
        {
            var command = new RemoveTrainerCommand(gymId, TrainerId);
            var DeleteTrainernresult = await _sender.Send(command);
            return DeleteTrainernresult.Match(
                _ => NoContent(),
                 Problem);

        }
    }
}
