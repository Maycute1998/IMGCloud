using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Utilities.TokenBuilder
{
    public static class JwtSecurityKey
    {
        /// <summary>
        /// SymmetricSecurityKey Create
        /// Create Key for Request/Respone APIs
        /// </summary>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static SymmetricSecurityKey Create(string secret)
        {
            //Return byte
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
