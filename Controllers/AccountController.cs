using System.Runtime.InteropServices;
using System.Security.Claims;
using API_tresure.Models;
using API_tresure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using tresure_api.Data;
using tresure_api.Data.Interfaces;

namespace tresure_api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        // private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<User> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            // _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<getLoginUser>> Login(PostLoginUser login)
        {
            User user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
                return Unauthorized();

            return new getLoginUser
            {
                Email = user.Email,
                Username = user.UserName,
                Token = await _tokenService.GenerateToken(user)
            };

        }
        [HttpGet("user")]
        public async Task<ActionResult<getUserDTO>> FindUserByEmail(string userEmail)
        {
            User user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
                return NotFound();


            return new getUserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName
            };

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUser register)
        {
            User user = new User() { UserName = register.Username, Email = register.Email };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            return StatusCode(201);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet("currentUser")]
        [Authorize]
        public async Task<ActionResult<getLoginUser>> GetCurrentUser()
        {
            string user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userManager.FindByIdAsync(user_id);

            return new getLoginUser
            {
                Email = user.Email,
                Username = user.UserName,
                Token = await _tokenService.GenerateToken(user)
            };
        }

    }
}
