using System.ComponentModel.DataAnnotations;

namespace IMGCloud.Infrastructure.Context;

public sealed class SignInContext
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
