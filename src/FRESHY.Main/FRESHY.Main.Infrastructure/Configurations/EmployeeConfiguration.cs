using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    private const string EMPLOYEE_TABLE = "Employees";
    private const string SCHEMA = "employee";

    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        ConfigEmployee(builder);
        AddJobPositionForeignKey(builder);
    }

    private static void AddJobPositionForeignKey(EntityTypeBuilder<Employee> builder)
    {
        builder.HasOne(employee => employee.JobPosition)
           .WithMany(jobPostion => jobPostion.Employees)
           .HasForeignKey(employee => employee.JobPositionId);
    }

    private static void ConfigEmployee(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(EMPLOYEE_TABLE, SCHEMA);

        builder.HasKey(employee => employee.Id);

        builder.Property(employee => employee.Id)
            .HasConversion(id => id.Value, value => EmployeeId.Create(value))
            .ValueGeneratedNever();

        builder.Property(employee => employee.AccountId);
        builder.HasIndex(employee => employee.AccountId)
            .IsUnique();

        builder.Property(employee => employee.Email);
        builder.HasIndex(employee => employee.Email)
            .IsUnique();

        builder.Property(employee => employee.Fullname)
            .HasMaxLength(255);

        builder.Property(employee => employee.PhoneNumber)
            .HasMaxLength(20);

        builder.HasIndex(employee => employee.PhoneNumber)
            .IsUnique();

        builder.Property(employee => employee.DOB)
            .HasColumnType("DATETIME");

        builder.Property(employee => employee.SSN)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(16);

        builder.Property(employee => employee.Hometown)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);

        builder.Property(employee => employee.CvLink)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);

        builder.Property(employee => employee.CreatedDate)
            .HasColumnType("DATETIME");

        builder.Property(employee => employee.UpdatedDate)
            .HasColumnType("DATETIME")
            .IsRequired(false);

        builder.Property(employee => employee.ManagerId)
            .HasConversion(
                    id => id.Value,
                    value => EmployeeId.Create(value))
            .ValueGeneratedNever()
            .IsRequired(false);

        builder.Property(employee => employee.LivingAddress)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(500);

        builder.Property(employee => employee.Avatar)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(1000);
    }
}