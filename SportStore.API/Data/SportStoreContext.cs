using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public SportStoreContext(DbContextOptions options) : base(options)
    {
        
    }

    public async Task CommitChangedAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);
    }

    // protected override void OnModelCreating(ModelBuilder builder){
            
    //         builder.ApplyConfigurationsFromAssembly(typeof(SportStoreContext).Assembly);

    //         builder.Entity<Role>().HasData(
    //             new Role(){ Id = 1, Name = "admin"}
    //         );

    // }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken){

            foreach(var entry in ChangeTracker.Entries<Base>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
    }


}