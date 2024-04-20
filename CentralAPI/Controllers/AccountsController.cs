using CentralAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CentralAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountsController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<ActionResult> Login([FromBody] Account account)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(account.Username, account.Password, account.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    //Generate Token
                    var token = Guid.NewGuid().ToString();

                    Response.Cookies.Append("MySessionCookie", token, new CookieOptions
                    {
                        Expires = account.RememberMe ? DateTimeOffset.Now.AddDays(7) : DateTimeOffset.Now.AddHours(1),
                        HttpOnly = true,
                        Secure = true
                    });

                    return Ok(new { message = "Login successful", token });
                }
                else
                {
                    return BadRequest(new { message = "Invalid login attempt" });
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("{Signup}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Signup([FromBody] Signup signupObj)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = signupObj.Username, PasswordHash = signupObj.Password };
                var result = await _userManager.CreateAsync(user, signupObj.Password);

                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
            

    }
}
