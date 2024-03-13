namespace IMGCloud.Domain.Entities;

public interface IDateTracking
{
    DateTime CreatedDate { get; set; }
    DateTime? ModifiedDate { get; set; }
}
