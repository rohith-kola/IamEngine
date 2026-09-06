// IamEngine.Domain/Entities/ProvisioningEvent.cs
namespace IamEngine.Domain.Entities;

public class ProvisioningEvent
{
    public Guid Id { get; set; }
    public Guid AppUserId { get; set; }
    public string ConnectorKey { get; set; } = default!;
    public string Action { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string? Details { get; set; }
    public DateTime OccurredAtUtc { get; set; }
}