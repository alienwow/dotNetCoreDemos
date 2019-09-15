using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace JwtAuth.Models
{
    public class UserStore
    {
        private static List<User> _users = new List<User>() {
            new User("1") {
                DisplayName="alice",
                Password="alice",
                AuthenticationTime = DateTime.Now.ToUniversalTime() ,
                PhoneNumber = "13838383888",
                Email="AliceSmith@email.com",
                AdditionalClaims = new List<Claim>{
                    new Claim(Constants.JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(Constants.JwtClaimTypes.GivenName, "Alice"),
                    new Claim(Constants.JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(Constants.JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(Constants.JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(Constants.JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(Constants.JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", "json")
                }
            },
            new User("2") {
                DisplayName="bob",
                Password="bob",
                AuthenticationTime = DateTime.Now.ToUniversalTime() ,
                PhoneNumber = "13838383888",
                Email="BobSmith@email.com",
                AdditionalClaims = new List<Claim>{
                    new Claim(Constants.JwtClaimTypes.Name, "Bob Smith"),
                    new Claim(Constants.JwtClaimTypes.GivenName, "Bob"),
                    new Claim(Constants.JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(Constants.JwtClaimTypes.Email, "BobSmith@email.com"),
                    new Claim(Constants.JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(Constants.JwtClaimTypes.WebSite, "http://bob.com"),
                    new Claim(Constants.JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", "json"),
                    new Claim("location", "somewhere")
                }
            }
        };

        public User FindUser(string userName, string password)
        {
            return _users.FirstOrDefault(_ => _.DisplayName == userName && _.Password == password);
        }
    }
}