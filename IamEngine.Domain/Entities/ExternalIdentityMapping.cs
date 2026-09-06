// IamEngine.Domain/Entities/ExternalIdentityMapping.cs
namespace IamEngine.Domain.Entities;

public class ExternalIdentityMapping
{
    public Guid Id { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = default!;
    public string ConnectorKey { get; set; } = default!;
    public string ExternalId { get; set; } = default!;
    public DateTime LastSyncedUtc { get; set; }
}