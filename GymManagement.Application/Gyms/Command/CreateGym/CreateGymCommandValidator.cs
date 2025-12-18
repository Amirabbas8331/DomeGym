
using FluentValidation;

namespace GymManagement.Application.Gyms.Command.CreateGym;

public class CreateGymCommandValidator:AbstractValidator<CreateGymCommand>
{
    public CreateGymCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(3, 20);
        RuleFor(x => x.subscriptionid).NotEmpty().NotNull();
    }
}
