using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace usingBearerAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.UserName == "admin" && model.Password == "123")
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aman-burasi-onay-icin-onemli"));
                    var cryptoCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email,"admin@system.com"),
                        new Claim(JwtRegisteredClaimNames.Name,"adminOfTheSystem"),

                    };
                    var token = new JwtSecurityToken(
                        issuer: "server",
                        audience: "client",
                        claims: claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddDays(7),
                        signingCredentials: cryptoCredential
                        );

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
                ModelState.AddModelError("login", "Hatalı giriş!");
            }
            return BadRequest(ModelState);
        }
    }
}
