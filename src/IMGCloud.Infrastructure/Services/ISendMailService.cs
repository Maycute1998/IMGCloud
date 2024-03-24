using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Services
{
    public interface ISendMailService
    {
        Task SendResetPasswordEmail(string email, string token);
    }
}
