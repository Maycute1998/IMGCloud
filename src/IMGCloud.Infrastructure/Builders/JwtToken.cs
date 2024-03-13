using System.IdentityModel.Tokens.Jwt;

namespace IMGCloud.Infrastructure.Builders
{
    /// <summary>
    /// Class Name  :   JwtToken
    /// Type Class  :   Static
    /// Handler Token key
    /// </summary>
    public sealed class JwtToken
    {
        private readonly JwtSecurityToken token;

        internal JwtToken(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime ValidTo => token.ValidTo;
        public string Value => new JwtSecurityTokenHandler().WriteToken(token);
        public string ExpiresDays => token.Claims.ToList().Where(x => x.Type.Equals("exp")).Select(x => x.Value).FirstOrDefault();
    }
}
