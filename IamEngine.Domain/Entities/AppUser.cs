using Microsoft.AspNetCore.Identity;

namespace IamEngine.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string DisplayName { get; set; } = default!;
    public UserSource Source { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAtUtc { get; set; }
    public ICollection<ExternalIdentityMapping> ExternalIdentities { get; set; } = new List<ExternalIdentityMapping>();
}

public enum UserSource { Local, EntraSso, Provisioned }