using IMGCloud.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Domain.Repositories.Implement
{
    public interface IUserTokenRepository
    {
        Task <ResponeVM> StoreTokenAsync(TokenVM tokenModel);
    }
}
