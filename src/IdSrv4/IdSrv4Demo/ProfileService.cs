//
// ProfileService.cs
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
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace IdSrv4Demo
{
    /// <summary>
    /// <see cref="ProfileService"/>
    /// </summary>
    public class ProfileService : IProfileService
    {
        public virtual async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.AddRequestedClaims(new[]
                            {
                                new Claim("name", "Alice"),
                                new Claim("website", "https://alice.com"),
                                new Claim(JwtClaimTypes.Role,"admin")
                            });

            ////判断是否有请求Claim信息
            //if (context.RequestedClaimTypes.Any())
            //{
            //    try
            //    {
            //        var id = long.Parse(context.Subject.Claims.Where(x => x.Type == JwtClaimTypes.Id).First().Value);
            //        //get user from db (find user by user id)
            //        var user = await (await _userRepository.Users.FindAsync<User>(new BsonDocument { { "_id", id } })).SingleOrDefaultAsync();

            //        // issue the claims for the user
            //        if (user != null)
            //        {
            //            var claims = ResourceOwnerPasswordValidator.GetUserClaims(user);
            //            context.AddRequestedClaims(claims);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogError(ex, ex.Message);
            //    }
            //}
        }

        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }
    }
}
