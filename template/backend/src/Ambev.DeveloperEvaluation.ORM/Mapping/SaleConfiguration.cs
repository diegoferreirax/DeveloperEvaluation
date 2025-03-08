using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sale");

        builder.HasKey(u => u.Id);
        builder.HasAlternateKey(a => a.SaleNumber);

        builder.Property(u => u.Id).HasColumnType("UUID").HasDefaultValueSql("GEN_RANDOM_UUID()");

        builder.Property(u => u.SaleNumber).IsRequired();
        builder.Property(u => u.SaleDate).IsRequired().HasColumnType("TIMESTAMP");
        builder.Property(u => u.TotalAmount).IsRequired().HasColumnType("NUMERIC(10,2)");
        builder.Property(u => u.IsCanceled);
        builder.Property(u => u.Branch).IsRequired().HasMaxLength(200);

        builder
            .HasOne(p => p.Customer)
            .WithMany()
            .HasForeignKey(f => f.CustomerId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
