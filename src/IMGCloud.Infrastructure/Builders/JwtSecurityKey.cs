using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IMGCloud.Infrastructure.Builders
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
