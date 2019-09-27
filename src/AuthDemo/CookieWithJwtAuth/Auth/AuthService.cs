using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using CookieWithJwtAuth.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CookieWithJwtAuth.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ILogger _logger;
        private readonly JwtOptions _jwtOptions;

        private const Token _emptyToken = null;

        public AuthService(
            ILogger<AuthService> logger,
            IOptions<JwtOptions> jwtOptions
        )
        {
            _jwtOptions = jwtOptions.Value;
            _logger = logger;
        }

        public async Task<Token> IssueToken(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var signinCredentials = new SigningCredentials(_jwtOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: new List<Claim>()
                {
                    new Claim(JwtClaimTypes.Id, user.SubjectId),
                    new Claim(JwtClaimTypes.Name, user.DisplayName)
                },
                expires: DateTime.Now.AddSeconds(_jwtOptions.Expires),
                signingCredentials: signinCredentials
            );

            return await Task.FromResult(new Token()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(tokeOptions),
                ExpiresIn = _jwtOptions.Expires,
                TokenType = Constants.Auth.JwtTokenType,
            });
        }

    }
}
