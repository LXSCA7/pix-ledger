namespace PixLedger.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}

public abstract class AuditableEntity : Entity
{
    public DateTime? DeletedAt { get; private set; } = null;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    
    public void Delete() => DeletedAt = DateTime.UtcNow;
    public void Update() =>  UpdatedAt = DateTime.UtcNow;
}