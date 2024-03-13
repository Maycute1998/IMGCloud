namespace IMGCloud.Infrastructure.Requests
{
    public class UserDetailsRequest
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Birthday { get; set; }

        public string? Photo { get; set; }

        public string? Bio { get; set; }
        public string? Link { get; set; }
        public string? Friend { get; set; }
        public string? Url { get; set; }
    }
}
