namespace GymManagement.Application.Validation;

public class ValidationException : Exception
{
    public IReadOnlyList<ValidationError> Errors { get; private set; }

    public ValidationException(IEnumerable<ValidationError> errors)
        : base("Validation failed")
    {
        Errors = errors.ToList();
    }
}
