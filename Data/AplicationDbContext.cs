using Microsoft.EntityFrameworkCore;
using udemy.Models;

namespace udemy.Data;

public class AplicationDbContext : DbContext
{
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Category> Categories { get; set; }
}