using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Item");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("UUID").HasDefaultValueSql("GEN_RANDOM_UUID()");

        builder.Property(u => u.Product).IsRequired().HasMaxLength(200);
        builder.Property(u => u.UnitPrice).IsRequired().HasColumnType("NUMERIC(10,2)");
    }
}
