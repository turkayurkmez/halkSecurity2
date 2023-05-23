using ClaimBasedAuthtantication.Models;
using ClaimBasedAuthtantication.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClaimBasedAuthtantication.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string gidilecekSayfa)
        {
            ViewBag.GidilecekSayfa = gidilecekSayfa;
            return View();

        }

        [HttpPost]
        public IActionResult Login([Bind("UserName", "Password")] UserLoginModel userLoginModel, string gidilecekSayfa)
        {
            if (ModelState.IsValid)
            {
                //db'den kontrol et.
                var loggedInUser = _userService.ValidateUser(userLoginModel.UserName, userLoginModel.Password);

                if (loggedInUser != null)
                {

                    //Authenticate olacak....
                    var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, loggedInUser.Name),
                        new Claim(ClaimTypes.Email, loggedInUser.Email),
                        new Claim(ClaimTypes.Role, loggedInUser.Role)
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    HttpContext.SignInAsync(claimsPrincipal);

                    if (!string.IsNullOrEmpty(gidilecekSayfa) && Url.IsLocalUrl(gidilecekSayfa))
                    {
                        return Redirect(gidilecekSayfa);
                    }
                    else
                    {
                        return Redirect("/");
                    }


                }

                ModelState.AddModelError("login", "Kullanıcı adı ya da Şifre hatalı");
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
