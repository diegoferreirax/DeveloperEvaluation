using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Extensions;

public static class DbContextExtensions
{
    public static void MapOnDeleteRestrictRelationships(this ModelBuilder builder)
    {
        foreach (var foreingKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            foreingKey.DeleteBehavior = DeleteBehavior.Restrict;
    }

    public static void SeedData(this ModelBuilder builder)
    {
        #region Customer
        var costumer1 = new Customer { Id = Guid.Parse("7bda9e04-f297-42f5-bd3b-c0fed49eacd4"), Name = "Alberto" };
        builder.Entity<Customer>().HasData(
            new Customer { Id = Guid.Parse("7bda9e04-f297-42f5-bd3b-c0fed49eacd4"), Name = "Alberto" },
            new Customer { Id = Guid.Parse("3b765f33-6d77-4da6-906f-511b1e2d009d"), Name = "Maria" }
        );
        #endregion

        #region Item
        builder.Entity<Item>().HasData(
            new Item { Id = Guid.Parse("5818a8f0-cd7c-4a5e-a7f2-a99507e9260d"), Product = "Brahma", UnitPrice = 2.00m },
            new Item { Id = Guid.Parse("c06a6875-7737-4558-a013-6acfb4e705c7"), Product = "Patagônia IPA", UnitPrice = 2.00m },
            new Item { Id = Guid.Parse("85c1e99d-4311-4fba-95c3-f327e34d3020"), Product = "Brahma DuploMaute", UnitPrice = 2.00m },
            new Item { Id = Guid.Parse("5ad99f20-db03-4d06-b539-28ece3792303"), Product = "Skol", UnitPrice = 2.00m },
            new Item { Id = Guid.Parse("2ccb7715-03fc-447f-9632-73a8a8bcc816"), Product = "Patagônia", UnitPrice = 2.00m }
        );
        #endregion
    }
}
