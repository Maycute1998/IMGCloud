namespace IMGCloud.Domain.Entities;

public interface IDateTracking
{
    DateTimeOffset CreatedDate { get; set; }
    DateTimeOffset? LastModifiedDate { get; set; }
}
