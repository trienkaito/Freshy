using FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate;
using FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class JobPositionConfiguration : IEntityTypeConfiguration<JobPosition>
{
    private const string JOB_POSITION_TABlE = "JobPositions";
    private const string SCHEMA = "employee";

    public void Configure(EntityTypeBuilder<JobPosition> builder)
    {
        ConfigJobPosition(builder);
    }

    private static void ConfigJobPosition(EntityTypeBuilder<JobPosition> builder)
    {
        builder.ToTable(JOB_POSITION_TABlE, SCHEMA);

        builder.HasKey(detail => detail.Id);

        builder.Property(detail => detail.Id)
            .HasConversion(id => id.Value, value => JobPositionId.Create(value))
            .ValueGeneratedNever();

        builder.Property(detail => detail.Name)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(155);
        builder.HasIndex(detail => detail.Name)
            .IsUnique();

        builder.Property(detail => detail.Description)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(detail => detail.Salary)
            .HasColumnType("MONEY");
    }
}