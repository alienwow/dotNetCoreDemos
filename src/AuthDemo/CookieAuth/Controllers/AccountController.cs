using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using CookieAuth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Text;

namespace CookieAuth.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel loginInputDto)
        {
            var userStore = HttpContext.RequestServices.GetService<UserStore>() as UserStore;
            var user = userStore.FindUser(loginInputDto.Username, loginInputDto.Password);
            if (user == null)
            {
                return View("LoginError");
            }
            else
            {
                AuthenticationProperties properties = null;
                if (loginInputDto.RememberLogin)
                {
                    properties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30))
                    };
                }

                // 在上面注册AddAuthentication时，指定了默认的Scheme，在这里便可以不再指定Scheme。
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user.CreatePrincipal(), properties);

                return Redirect(string.IsNullOrEmpty(loginInputDto.ReturnUrl) ? "/" : loginInputDto.ReturnUrl);
            }
        }

        [HttpGet]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Response.Redirect("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            if (HttpContext.Request.Cookies.TryGetValue(Constants.Auth.CookieAuthName, out string cookieValue))
            {
                var protectedData = Base64UrlTextEncoder.Decode(cookieValue);
                var dataProtectionProvider = HttpContext.RequestServices.GetRequiredService<IDataProtectionProvider>();
                var _dataProtector = dataProtectionProvider
                                        .CreateProtector(
                                            "Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware",
                                            CookieAuthenticationDefaults.AuthenticationScheme,
                                            "v2");
                var unprotectedData = _dataProtector.Unprotect(protectedData);

                string cookieData = Encoding.UTF8.GetString(unprotectedData);
                ViewData["cookieData"] = cookieData;
            }
            else
                ViewData["cookieData"] = "无数据";
            return View(HttpContext.User);
        }
    }
}