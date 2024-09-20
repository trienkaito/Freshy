using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FRESHY.Main.Infrastructure.Configurations;

public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    private const string VOUCHER_TABLE = "Vouchers";
    private const string SCHEMA = "order";

    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable(VOUCHER_TABLE, SCHEMA);

        builder.HasKey(voucher => voucher.Id);

        builder.Property(voucher => voucher.Id)
            .HasConversion(id => id.Value, value => VoucherId.Create(value))
            .ValueGeneratedNever();

        builder.Property(voucher => voucher.StartedOn)
            .HasColumnType("DATETIME");

        builder.Property(voucher => voucher.EndedOn)
            .HasColumnType("DATETIME");

        builder.OwnsOne(voucher => voucher.VoucherCode, voucherBuilder =>
        {
            voucherBuilder.Property(code => code.Value)
                .HasColumnName("Code")
                .HasColumnType("VARCHAR")
                .HasMaxLength(10);

            voucherBuilder.HasIndex(code => code.Value)
                .IsUnique();
        });

        builder.Property(voucher => voucher.Description)
            .HasColumnType(typeName: "NVARCHAR")
            .HasMaxLength(maxLength: 256);
    }
}