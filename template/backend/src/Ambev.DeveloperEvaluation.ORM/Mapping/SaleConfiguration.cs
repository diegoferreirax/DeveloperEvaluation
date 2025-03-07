using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

/*
 TODO: 
    ver oq da pra melhorar
 */
public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sale");

        builder.HasKey(u => u.Id);
        builder.HasAlternateKey(a => a.SaleNumber);

        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.SaleNumber).IsRequired();
        builder.Property(u => u.SaleDate).IsRequired().HasColumnType("timestamp");
        builder.Property(u => u.TotalAmount).IsRequired().HasColumnType("numeric(7,2)");
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
