namespace WebApi.DataAccess.SqlServer;

using Microsoft.EntityFrameworkCore;
using WebApi.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>().HasKey(p => p.PersonId);
        
        modelBuilder.Entity<Person>().ToTable("Person");
    }
}