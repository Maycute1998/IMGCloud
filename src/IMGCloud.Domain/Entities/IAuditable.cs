namespace IMGCloud.Domain.Entities;

public interface IAuditable : IDateTracking
{
    Status? Status { get; set; }
}

public enum Status
{
    InActive,
    Active
}
