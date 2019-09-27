using Microsoft.AspNetCore.Mvc;
using CookieWithJwtAuth.Models;
using CookieWithJwtAuth.Dtos;
using CookieWithJwtAuth.Auth;
using System.Threading.Tasks;

namespace CookieWithJwtAuth.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountApiController : Controller
    {
        private UserStore _store;
        private IAuthService _authService;

        public AccountApiController(UserStore store, IAuthService authService)
        {
            _store = store;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody]UserDto userDto)
        {
            var user = _store.FindUser(userDto.UserName, userDto.Password);
            if (user == null) return Unauthorized();
            return Ok(await _authService.IssueToken(user));
        }
    }
}
