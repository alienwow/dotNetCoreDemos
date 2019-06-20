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
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[] {
                //new ApiResource("IdSrv4Demo.Api", "My API")
                 new ApiResource("IdSrv4Demo.Api", "My API", RequiredUserClaims)
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
                    AllowedScopes = { "IdSrv4Demo.Api" }
                },
                #endregion
                        
                #region Implicit
                //new Client
                //{
                //    ClientId = "mvc",
                //    ClientName = "MVC Client",
                //    AllowedGrantTypes = GrantTypes.Implicit,

                //    // where to redirect to after login
                //    RedirectUris = { "https://localhost:5002/signin-oidc" },

                //    // where to redirect to after logout
                //    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                //    AllowedScopes = new List<string>
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile
                //    }
                //},
                #endregion

                #region Hybrid
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                        
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "IdSrv4Demo.Api"
                    },
                    AllowOfflineAccess = true
                },
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
            return new List<TestUser>
                    {
                        new TestUser
                        {
                            SubjectId = "1",
                            Username = "alice",
                            Password = "alice",

                            Claims = new []
                            {
                                new Claim("name", "Alice"),
                                new Claim("website", "https://alice.com"),
                                new Claim(JwtClaimTypes.Role,"admin")
                            }
                        },
                        new TestUser
                        {
                            SubjectId = "2",
                            Username = "bob",
                            Password = "bob",

                            Claims = new []
                            {
                                new Claim("name", "Bob"),
                                new Claim("website", "https://bob.com"),
                                new Claim(JwtClaimTypes.Role,"general")
                            }
                        }
                    };
        }
    }
}
