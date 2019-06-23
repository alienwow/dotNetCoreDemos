//
// Config.cs
//
// Author:
//       Vito <wuwenhao0327@gmail.com>
//
// Copyright (c) 2019 Vito
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System.Collections.Generic;
using System.Security.Claims;

using IdentityModel;

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;


public static class CustomIdentityServerConstants
{
    public static class CustomScopes
    {
        public const string Role = "role";
    }
}

public static class CustomIdentityResources
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IdentityServer4.Models.IdentityResource" />
    public class Role : IdentityResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
        {
            Name = CustomIdentityServerConstants.CustomScopes.Role;
            DisplayName = "Your role";
            Description = "Your account role information (admin, general, etc.)";
            Emphasize = true;
            UserClaims = new List<string>()
                        {
                            "role"
                        };
        }
    }
}

namespace IdSrv4Demo
{
    public static class Config
    {

        private readonly static ICollection<string> RequiredUserClaims =
            new string[] {
                        JwtClaimTypes.Id,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.PhoneNumber,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Gender,
                        JwtClaimTypes.Picture
                    };


        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new CustomIdentityResources.Role()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[] {
                 new ApiResource("IdSrv4Demo.Api", "My API"),
                 new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[] {

                #region ClientCredentials
                new Client{
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "IdSrv4Demo.Api" }
                },
                #endregion
                    
                #region ResourceOwnerPassword
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // scopes that client has access to
                    AllowedScopes = { "IdSrv4Demo.Api", IdentityServerConstants.LocalApi.ScopeName }
                },
                #endregion
                        
                #region Implicit
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        CustomIdentityServerConstants.CustomScopes.Role
                    }
                },
                #endregion

                #region Hybrid
                //new Client
                //{
                //    ClientId = "mvc",
                //    ClientName = "MVC Client",
                //    AllowedGrantTypes = GrantTypes.Hybrid,

                //    ClientSecrets =
                //    {
                //        new Secret("secret".Sha256())
                //    },
                        
                //    // where to redirect to after login
                //    RedirectUris = { "https://localhost:5002/signin-oidc" },

                //    // where to redirect to after logout
                //    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        "IdSrv4Demo.Api"
                //    },
                //    AllowOfflineAccess = true
                //},
                #endregion
                
                #region js
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =           { "https://localhost:5007/callback.html" },
                    PostLogoutRedirectUris = { "https://localhost:5007/index.html" },
                    AllowedCorsOrigins =     { "https://localhost:5007" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "IdSrv4Demo.Api"
                    }
                }
                #endregion
            };
        }

        public static List<TestUser> GetUsers()
        {
            return
            new List<TestUser>
            {
                new TestUser{
                    SubjectId = "1",
                    Username = "alice",
                    Password = "alice",

                    Claims =
                    {
                        #region OpenId
                        // 默认必须会带上 JwtClaimTypes.Subject 了，所以不需要再手动添加
                        #endregion
                        
                        #region Profile
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.MiddleName,"middle_name"),
                        new Claim(JwtClaimTypes.NickName,"nickname"),
                        new Claim(JwtClaimTypes.PreferredUserName,"preferred_username"),
                        new Claim(JwtClaimTypes.Profile,"profile"),
                        new Claim(JwtClaimTypes.Picture,"picture"),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Gender,"gender"),
                        new Claim(JwtClaimTypes.BirthDate,"birthdate"),
                        new Claim(JwtClaimTypes.ZoneInfo,"zoneinfo"),
                        new Claim(JwtClaimTypes.Locale,"locale"),
                        new Claim(JwtClaimTypes.UpdatedAt,"updated_at"),
                        #endregion
                        
                        #region Email
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        #endregion
                        
                        #region Phone
                        new Claim(JwtClaimTypes.PhoneNumber, "13838383888"),
                        new Claim(JwtClaimTypes.PhoneNumberVerified, "true", ClaimValueTypes.Boolean),
                        #endregion
                        
                        #region Address
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServerConstants.ClaimValueTypes.Json),
                        #endregion
                        
                        #region Others Customize
                        new Claim(JwtClaimTypes.Role,"admin")
                        #endregion
                    }
                },
                new TestUser{
                    SubjectId = "2",
                    Username = "bob",
                    Password = "bob",

                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),

                        
                        #region Email
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com", ClaimValueTypes.Email),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        #endregion
                        
                        #region Phone
                        new Claim(JwtClaimTypes.PhoneNumber, "13838383888"),
                        new Claim(JwtClaimTypes.PhoneNumberVerified, "true", ClaimValueTypes.Boolean),
                        #endregion
                        
                        #region Address
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        #endregion
                        
                        #region Others Customize
                        new Claim(JwtClaimTypes.Role,"general")
                        #endregion
                    }
                }
            };
        }
    }
}
