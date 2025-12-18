using ErrorOr;
using GymManagement.Domain.ValueObjects;
using Throw;
namespace GymManagement.Domain.Trainers;

public class Trainer
{
    public Guid Id { get; private set; }
    public TrainerName FullName { get; private set; } = null!;
    public string? Specialty { get; private set; }

    private Trainer() { }

    private Trainer(TrainerName fullName, string? specialty, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        FullName = fullName;
        Specialty = specialty;
    }

    public static ErrorOr<Trainer> Create(TrainerName fullName, string? specialty = null)
    {
        fullName.Throw().IfNullOrWhiteSpace(x=>x.ThrowIfNull().ToString());

        return new Trainer(fullName, specialty);
    }

    public ErrorOr<Success> UpdateSpecialty(string? specialty)
    {
        Specialty = specialty;
        return Result.Success;
    }
}

