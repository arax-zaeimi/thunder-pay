using Microsoft.EntityFrameworkCore;
using ThunderPay.Entities.Entities;

namespace ThunderPay.Entities;

public class ThunderPayDbContext(DbContextOptions<ThunderPayDbContext> options) 
    : DbContext(options)
{
    public DbSet<MerchantDbm> Merchants { get; set; }

    public DbSet<OrganizationDbm> Organizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        MerchantDbm.Configure(modelBuilder.Entity<MerchantDbm>());
    }
}
