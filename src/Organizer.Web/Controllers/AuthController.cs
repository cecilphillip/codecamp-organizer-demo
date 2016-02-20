using Microsoft.AspNet.Mvc;
using Organizer.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Organizer.Web.Controllers
{
    public class AuthController : Controller
    {       
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {  
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid || model.UserName != model.Password) {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Country, "USA" ),
                new Claim("Event", "South Florida Code Camp 2016")
            };

            if (model.UserName.Equals("admin", StringComparison.OrdinalIgnoreCase)) {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }

            var identity = new ClaimsIdentity(claims, "local", ClaimTypes.Name, ClaimTypes.Role);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.Authentication.SignInAsync("Cookies", principal);

            return RedirectToLocal(returnUrl);
        }

        [HttpGet]
        public IActionResult Denied()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");            
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
