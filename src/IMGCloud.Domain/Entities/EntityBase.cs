
namespace IMGCloud.Domain.Entities;

public abstract class EntityBase<TKey> : IEntityBase<TKey>, IAuditable
{
    public TKey Id { get; set; }
    public Status? Status { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? LastModifiedDate { get; set; }
}
