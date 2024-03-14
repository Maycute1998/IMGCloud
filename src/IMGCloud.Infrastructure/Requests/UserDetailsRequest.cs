namespace IMGCloud.Infrastructure.Requests
{
    public sealed class UserDetailsRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }

        public string? Photo { get; set; }

        public string? Bio { get; set; }
        public string? Link { get; set; }
        public string? Friend { get; set; }
        public string? Url { get; set; }
    }
}
