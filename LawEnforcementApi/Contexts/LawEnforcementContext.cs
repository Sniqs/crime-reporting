using LawEnforcementApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LawEnforcementApi.Contexts;

public class LawEnforcementContext : DbContext
{
    public DbSet<Officer> Officers { get; set; } = null!;
    public DbSet<Rank> Ranks { get; set; } = null!;
    public DbSet<CrimeEvent> CrimeEvents { get; set; } = null!;

    public LawEnforcementContext(DbContextOptions<LawEnforcementContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Officer>(b =>
        {
            b.Property(o => o.CallSign).HasMaxLength(20);
            b.HasIndex(o => o.CallSign).IsUnique();
        });

        builder.Entity<Rank>(b => b.Property(r => r.Name).HasMaxLength(50));
    }
}


