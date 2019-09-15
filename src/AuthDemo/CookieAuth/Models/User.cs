using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CookieAuth.Models
{
    /// <summary>
    /// Model properties of an IdentityServer user
    /// </summary>
    public class User
    {
        /// <summary>
        /// Subject ID (mandatory)
        /// </summary>
        public string SubjectId { get; }

        /// <summary>
        /// Display name (optional)
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Identity provider (optional)
        /// </summary>
        public string IdentityProvider { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Authentication methods
        /// </summary>
        public ICollection<string> AuthenticationMethods { get; set; } = new HashSet<string>();

        /// <summary>
        /// Authentication time
        /// </summary>
        public DateTime? AuthenticationTime { get; set; }

        /// <summary>
        /// Additional claims
        /// </summary>
        public ICollection<Claim> AdditionalClaims { get; set; } = new HashSet<Claim>(new ClaimComparer());

        /// <summary>
        /// Initializes a user identity
        /// </summary>
        /// <param name="subjectId">The subject ID</param>
        public User(string subjectId)
        {
            if (string.IsNullOrEmpty(subjectId)) throw new ArgumentException("SubjectId is mandatory", nameof(subjectId));

            SubjectId = subjectId;
        }

        /// <summary>
        /// Creates an IdentityServer claims principal
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ClaimsPrincipal CreatePrincipal()
        {
            if (string.IsNullOrEmpty(SubjectId)) throw new ArgumentException("SubjectId is mandatory", nameof(SubjectId));
            var claims = new List<Claim> { new Claim(Constants.JwtClaimTypes.Subject, SubjectId) };

            if (!string.IsNullOrEmpty(DisplayName))
            {
                claims.Add(new Claim(Constants.JwtClaimTypes.Name, DisplayName));
            }

            if (!string.IsNullOrEmpty(IdentityProvider))
            {
                claims.Add(new Claim(Constants.JwtClaimTypes.IdentityProvider, IdentityProvider));
            }

            if (AuthenticationTime.HasValue)
            {
                claims.Add(new Claim(Constants.JwtClaimTypes.AuthenticationTime, new DateTimeOffset(AuthenticationTime.Value).ToUnixTimeSeconds().ToString()));
            }

            if (AuthenticationMethods.Any())
            {
                foreach (var amr in AuthenticationMethods)
                {
                    claims.Add(new Claim(Constants.JwtClaimTypes.AuthenticationMethod, amr));
                }
            }

            claims.AddRange(AdditionalClaims);

            var id = new ClaimsIdentity(
                claims.Distinct(new ClaimComparer()),
                Constants.Auth.AuthenticationType,
                Constants.JwtClaimTypes.Name,
                Constants.JwtClaimTypes.Role);
            return new ClaimsPrincipal(id);
        }
    }
}