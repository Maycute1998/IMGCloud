﻿using System.ComponentModel.DataAnnotations;

namespace IMGCloud.Infrastructure.Requests;

public class CreateUserRequest
{
    public int Id { get; set; }
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
