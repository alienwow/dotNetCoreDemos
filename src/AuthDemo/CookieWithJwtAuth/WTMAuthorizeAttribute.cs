using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace CookieWithJwtAuth.Auth
{
    public class WTMAuthorizeAttribute : AuthorizeAttribute
    {
        public WTMAuthorizeAttribute()
        {
            // AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme + "," +
            //                         JwtBearerDefaults.AuthenticationScheme;
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + "," +
                                    CookieAuthenticationDefaults.AuthenticationScheme;

        }
    }
}
