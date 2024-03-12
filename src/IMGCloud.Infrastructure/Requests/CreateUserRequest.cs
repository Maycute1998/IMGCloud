using System.ComponentModel.DataAnnotations;

namespace IMGCloud.Infrastructure.Requests;

public record CreateUserRequest([Required, MaxLength(255)] string UserName, [Required, MaxLength(255)] string Password, [Required, EmailAddress] string Email)
{
}
