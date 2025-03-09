using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItem");

        builder.HasKey(u => u.Id);
        builder.HasAlternateKey(a => new { a.SaleId, a.ItemId });

        builder.Property(u => u.Id).HasColumnType("UUID").HasDefaultValueSql("GEN_RANDOM_UUID()");

        builder.Property(u => u.Quantity).IsRequired();
        builder.Property(u => u.Discount).HasColumnType("NUMERIC(5,2)");
        builder.Property(u => u.TotalItemAmount).IsRequired().HasColumnType("NUMERIC(10,2)");

        builder
            .HasOne(o => o.Sale)
            .WithMany()
            .HasForeignKey(f => f.SaleId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(o => o.Item)
            .WithMany()
            .HasForeignKey(f => f.ItemId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
