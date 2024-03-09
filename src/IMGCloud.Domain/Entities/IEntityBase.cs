namespace IMGCloud.Domain.Entities;

public interface IEntityBase<TKey>
{
    TKey Id { get; set; }
}
