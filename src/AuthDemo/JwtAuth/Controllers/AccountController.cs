using Microsoft.AspNetCore.Mvc;
using JwtAuth.Models;
using JwtAuth.Dtos;
using JwtAuth.Auth;
using System.Threading.Tasks;

namespace JwtAuth.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : Controller
    {
        private UserStore _store;
        private IAuthService _authService;

        public AccountController(UserStore store, IAuthService authService)
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
