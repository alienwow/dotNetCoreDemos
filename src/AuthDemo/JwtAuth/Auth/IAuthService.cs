using System.Threading.Tasks;
using JwtAuth.Models;

namespace JwtAuth.Auth
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
