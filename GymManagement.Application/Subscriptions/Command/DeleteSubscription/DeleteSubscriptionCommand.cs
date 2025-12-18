using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;


namespace GymManagement.Application.Subscriptions.Command.DeleteSubscription;

public record DeleteSubscriptionCommand(Guid SubscriptionId):ICommandBase<ErrorOr<Deleted>>;
