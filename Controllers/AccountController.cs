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
using tresure_api.Migrations;

namespace tresure_api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        // private readonly SignInManager<User> _signInManager;
        private readonly AppDbContext _context;

        private readonly TokenService _tokenService;

        public AccountController(UserManager<User> userManager, TokenService tokenService, AppDbContext context)
        {
            _userManager = userManager;
            // _signInManager = signInManager;
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<getLoginUser>> Login(PostLoginUser login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
                return Unauthorized();

            return new getLoginUser
            {
                Email = user.Email,
                Username = user.UserName,
                Token = await _tokenService.GenerateToken(user)
            };

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUser register)
        {
            var user = new User() { UserName = register.Username, Email = register.Email };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            await _userManager.AddToRoleAsync(user, UserRoles.User);

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
            var user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(user_id);

            System.Console.WriteLine(user);
            return new getLoginUser
            {
                Email = user.Email,
                Username = user.UserName,
                Token = await _tokenService.GenerateToken(user)
            };
        }

    }
}
