// IamEngine.Domain/Entities/ConnectorConfig.cs
namespace IamEngine.Domain.Entities;

public class ConnectorConfig
{
    public Guid Id { get; set; }
    public string Key { get; set; } = default!;
    public string ConnectorType { get; set; } = default!;
    public string ConfigJson { get; set; } = default!;
    public bool Enabled { get; set; } = true;
}