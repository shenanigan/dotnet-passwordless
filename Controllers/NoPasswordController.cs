using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NoPasswordProject.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NoPasswordController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public NoPasswordController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<String>> Login([FromQuery] string Email)
        {
            // Create or Fetch your user from the database
            var User = await _userManager.FindByNameAsync(Email);
            if (User == null)
            {
                User = new IdentityUser();
                User.Email = Email;
                User.UserName = Email;
                var IdentityResult = await _userManager.CreateAsync(User);
                if (IdentityResult.Succeeded == false)
                {
                    return BadRequest();
                }
            }

            var Token = await _userManager.GenerateUserTokenAsync(User, "NPTokenProvider", "nopassword-for-the-win");
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<String>> Verify([FromQuery] string Token, [FromQuery] string Email)
        {
            // Fetch your user from the database
            var User = await _userManager.FindByNameAsync(Email);
            if (User == null)
            {
                return NotFound();
            }

            var IsValid = await _userManager.VerifyUserTokenAsync(User, "NPTokenProvider", "nopassword-for-the-win", Token);
            if (IsValid)
            {
                // TODO: Generate a bearer token
                var BearerToken = "";
                return BearerToken;
            }
            return Unauthorized();
        }
    }
}
