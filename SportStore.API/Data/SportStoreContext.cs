using Microsoft.EntityFrameworkCore;
using SportStore.API.Entities;

namespace SportStore.API.Data;

public class SportStoreContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SportStoreCourse;Username=postgres;Password=root");
    }
}