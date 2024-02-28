using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Domain.Models
{
    public class CreateUserVM
    {
        [Required(ErrorMessage = "userNameRequired")]
        [StringLength(255, ErrorMessage = "userNameLimitStringLength")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "passwordRequired")]
        [StringLength(255, ErrorMessage = "passwordLimitStringLength")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "emailRequired")]
        [StringLength(255, ErrorMessage = "emailLimitStringLength")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "emailNotValid")]
        public string? Email { get; set; }
    }
}
