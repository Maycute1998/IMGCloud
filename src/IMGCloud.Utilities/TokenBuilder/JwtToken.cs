using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Utilities.TokenBuilder
{
    /// <summary>
    /// Class Name  :   JwtToken
    /// Type Class  :   Static
    /// Handler Token key
    /// </summary>
    public sealed class JwtToken
    {
        private JwtSecurityToken token;

        internal JwtToken(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime ValidTo => token.ValidTo;
        public string Value => new JwtSecurityTokenHandler().WriteToken(token);
        public string ExpiresDays => token.Claims.ToList().Where(x => x.Type.Equals("exp")).Select(x => x.Value).FirstOrDefault();
    }
}
