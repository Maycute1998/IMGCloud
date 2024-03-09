namespace IMGCloud.Domain.Entities;

public class UserInfo : EntityBase<int>
{
    public int UserId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? FullName { get; set; }

    public string? Photo { get; set; }

    public string? Bio { get; set; }
    public string? Link { get; set; }
    public string? Friend { get; set; }

    public string? Url { get; set; }

    public DateTime? BirthDay { get; set; }

    public string? Address { get; set; }

    public User? User { get; set; }
}
