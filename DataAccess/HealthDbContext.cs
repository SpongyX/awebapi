using awebapi.Entities;
using Microsoft.EntityFrameworkCore;

public class HealthDbContext : DbContext
{
    public HealthDbContext(DbContextOptions<HealthDbContext> options)
        : base(options)
    {
    }

    public DbSet<Users> Users { get; set; }
     public DbSet<Medicines> Medicines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Users>(entity =>
        {
            entity.Property(e => e.User_id)
                  .HasDefaultValueSql("uuid_generate_v4()"); 
        });

              modelBuilder.Entity<Medicines>(entity =>
        {
            entity.Property(e => e.Med_id)
                  .HasDefaultValueSql("uuid_generate_v4()"); 
        });
    }
}
