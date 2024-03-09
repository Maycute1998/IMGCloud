using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace IMGCloud.Utilities.TokenBuilder
{
    public sealed class JwtTokenBuilder
    {
        private SecurityKey securityKey = null;

        
        private string UserName = string.Empty;
        private string subject = string.Empty;
        private string issuer = string.Empty;
        private string audience = string.Empty;
        private Dictionary<string, string> claims = new Dictionary<string, string>();
        private int expiryInMinutes = 5;
        private int expiryDate = 10;

        public JwtTokenBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this.securityKey = securityKey;
            return this;
        }
        public JwtTokenBuilder AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }
        public JwtTokenBuilder AddUserName(string UserName)
        {
            this.UserName = UserName;
            return this;
        }
        public JwtTokenBuilder AddIssuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }
        public JwtTokenBuilder AddAudience(string audience)
        {
            this.audience = audience;
            return this;
        }
        public JwtTokenBuilder AddClaim(string type, string value)
        {
            claims.Add(type, value);
            return this;
        }
        public JwtTokenBuilder AddClaims(Dictionary<string, string> claims)
        {
            this.claims.Union(claims);
            return this;
        }
        public JwtTokenBuilder AddExpiry(int expiryInMinutes)
        {
            this.expiryInMinutes = expiryInMinutes;
            return this;
        }
        public JwtTokenBuilder AddExpiryDate(int expiryDate)
        {
            this.expiryDate = expiryDate;
            return this;
        }

        /// <summary>
        /// GenerateAccessToken contains the logic to generate the access token
        /// </summary>
        /// <returns> The access token</returns>
        public JwtToken GenerateAccessToken(bool isUseDate = false)
        {
            EnsureArguments();
            var temp = DateTime.Now.AddMinutes(expiryInMinutes).ToString();
            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub, subject),//Set username
              new Claim(JwtRegisteredClaimNames.Email, UserName),//Set Email
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              new Claim(JwtRegisteredClaimNames.UniqueName,UserName)
              //new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.AddMinutes(expiryInMinutes).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer),
            }
            .Union(this.claims.Select(item => new Claim(item.Key, item.Value)));
            var nowUtc = DateTime.Now.ToUniversalTime();

            var token = new JwtSecurityToken(
                              issuer: issuer,
                              audience: audience,
                              claims: claims,
                              expires: isUseDate ? DateTime.UtcNow.AddDays(expiryDate)
                              : nowUtc.AddMinutes(expiryInMinutes).ToUniversalTime(),
                              signingCredentials: new SigningCredentials(
                                                        securityKey,
                                                        SecurityAlgorithms.HmacSha256));

            return new JwtToken(token);
        }

        /// <summary>
        /// GenerateRefreshToken() contains the logic to generate the refresh token. We
        /// </summary>
        /// <returns> returns the ClaimsPrincipal object</returns>
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        /// <summary>
        /// GetPrincipalFromExpiredToken is used to get the user principal from the expired access token.
        /// </summary>
        /// <returns> returns the ClaimsPrincipal object</returns>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(IConfiguration _configuration, string token)
        {
            var validIssuer = _configuration["TokenConfigs:Issuer"];
            var validAudience = _configuration["TokenConfigs:Audience"];
            var signingKey = _configuration["TokenConfigs:SecurityKey"];
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                SaveSigninToken = true,

                ValidIssuer = validIssuer,
                ValidAudience = validAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
        #region[Validate]
        private void EnsureArguments()
        {
            if (securityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrEmpty(issuer))
                throw new ArgumentNullException("Issuer");

            if (string.IsNullOrEmpty(audience))
                throw new ArgumentNullException("Audience");
        }
        #endregion
    }
}
