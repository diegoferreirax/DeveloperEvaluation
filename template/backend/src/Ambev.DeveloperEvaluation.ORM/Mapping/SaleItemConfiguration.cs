using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

/*
 TODO: 
    ver oq da pra melhorar
 */
public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItem");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Quantity).IsRequired();
        builder.Property(u => u.Discount).HasColumnType("numeric(7,2)");

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
