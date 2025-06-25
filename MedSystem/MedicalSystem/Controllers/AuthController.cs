using BL.IServices;
using MedicalSystem.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MedicalSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly IDoctorAuthService _authService;

        public AuthController(IDoctorAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                var jwt = _authService.Login(vm.Email, vm.Password);

                // 2) parse it to extract the claims
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);

                // 3) build a ClaimsIdentity out of those claims
                var identity = new ClaimsIdentity(token.Claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // 4) issue the cookie
                var props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(4)
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    props);

                return RedirectToAction("Index", "Patient");
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("not activated", StringComparison.OrdinalIgnoreCase)
           || ex.Message.Contains("not properly initialized", StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("CompleteSignup");
                }

                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult CompleteSignup()
        {
            var vm = new CompleteSignUpVM();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompleteSignup(CompleteSignUpVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                _authService.CompleteSignup(vm.Email, vm.Token, vm.NewPassword);
                return RedirectToAction("Login");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }
        [HttpGet]
        public IActionResult Forbidden()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("AuthToken");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

    }
}
