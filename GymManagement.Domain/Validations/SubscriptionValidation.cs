

using FluentValidation;
using FluentValidation.Results;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Domain.Validations;

public class SubscriptionValidation:AbstractValidator<Subscription>
{
    public SubscriptionValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().Length(5, 10).WithMessage("Please specify a first name");
        RuleFor(x => x.LastName).NotEmpty().Length(7,12).WithMessage("Please specify a Last Name");
        RuleFor(x => x.subscriptionType).NotEmpty().IsInEnum();
    }
}
