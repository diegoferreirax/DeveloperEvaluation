using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("UUID").HasDefaultValueSql("GEN_RANDOM_UUID()");

        builder.Property(u => u.Name).IsRequired().HasMaxLength(150);
    }
}
