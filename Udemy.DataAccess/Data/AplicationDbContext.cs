using Microsoft.EntityFrameworkCore;
using udemy.Models;

namespace Udemy.DataAccess.Data;

public class AplicationDbContext : DbContext
{
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
            new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
            new Category { Id = 3, Name = "History", DisplayOrder = 3 }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product()
            {
                Id = 1,
                Name = "Name1",
                Author = "author1",
                Description = "description1",
                Isbn = "isbn1",
                Price = 1,
                Price50 = 2,
                Price100 = 3,
                ListPrice = 4,
                ImageUrl = ""

            },
            new Product
            {
                Id = 2,
                Name = "Name2",
                Author = "author2",
                Description = "description2",
                Isbn = "isbn2",
                Price = 5,
                Price50 = 6,
                Price100 = 7,
                ListPrice = 8,
                ImageUrl = ""

            },
            new Product
            {
                Id = 3,
                Name = "Name3",
                Author = "author3",
                Description = "description3",
                Isbn = "isbn3",
                Price = 9,
                Price50 = 10,
                Price100 = 11,
                ListPrice = 12,
                ImageUrl = ""
            }
        );
    }
}