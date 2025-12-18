using GymManagement.Domain.Gyms;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GymManagement.Infrastructure.Common.Persistence;

namespace GymManagement.Infrastructure.Gyms.Persistence;

public class GymConfigurations : IEntityTypeConfiguration<Gym>
{
    public void Configure(EntityTypeBuilder<Gym> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .ValueGeneratedNever();
        builder.Property(g => g.Name);

        builder.Property(g => g.SubscriptionId);

        builder.Property(g => g.MaxRooms)
        .HasColumnName("MaxRooms");

        builder.Property(g => g.RoomIds)
     .HasColumnName("RoomIds")
     .HasListOfIdsConverter();

        builder.Property(g => g.TrainerIds)
            .HasColumnName("TrainerIds")
            .HasListOfIdsConverter();


        
    }
}
