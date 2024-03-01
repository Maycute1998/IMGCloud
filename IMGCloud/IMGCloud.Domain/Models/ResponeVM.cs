using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Domain.Models
{
    public class SigInVM
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }
    }
    public class SignUpVM
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
    public class ResponeVM
    {
        public bool Status { get; set; }

        public string? Message { get; set; }
    }

    public class ResponeAuthVM : ResponeVM
    {
        public string? Token { get; set; }
    }

    public class TokenVM
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpireDays { get; set; }
        public bool IsActive { get; set; }
    }
}
