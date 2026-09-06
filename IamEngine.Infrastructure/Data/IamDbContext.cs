// IamEngine.Infrastructure/Data/IamDbContext.cs
using IamEngine.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IamEngine.Infrastructure.Data;

public class IamDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public IamDbContext(DbContextOptions<IamDbContext> options) : base(options) { }

    public DbSet<ExternalIdentityMapping> ExternalIdentities => Set<ExternalIdentityMapping>();
    public DbSet<ConnectorConfig> ConnectorConfigs => Set<ConnectorConfig>();
    public DbSet<ProvisioningEvent> ProvisioningEvents => Set<ProvisioningEvent>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ExternalIdentityMapping>()
            .HasIndex(m => new { m.ConnectorKey, m.ExternalId })
            .IsUnique();

        builder.Entity<ConnectorConfig>()
            .HasIndex(c => c.Key)
            .IsUnique();
    }
}