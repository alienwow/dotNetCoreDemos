using System.Threading.Tasks;
using CookieWithJwtAuth.Models;

namespace CookieWithJwtAuth.Auth
{
    public interface IAuthService
    {
        /// <summary>
        /// Issue token
        /// </summary>
        /// <param name="loginUserInfo"></param>
        /// <returns></returns>
        Task<Token> IssueToken(User user);
    }
}
