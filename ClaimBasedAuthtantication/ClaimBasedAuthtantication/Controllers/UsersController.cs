using ClaimBasedAuthtantication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClaimBasedAuthtantication.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login([Bind("UserName", "Password")] UserLoginModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                //db'den kontrol et.
            }
            return View();
        }



    }
}
