using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Simply_Watered.Data;
using Simply_Watered.Models;
using Simply_Watered.Services.Interfaces;
using Simply_Watered.ViewModels;

namespace Simply_Watered.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtService jwtService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            var result= await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (result.IsLockedOut)
            {
                return BadRequest("User is Locked Out");
            }

            if (result.Succeeded)
            {
                var generatedToken = await _jwtService.GenerateJWTTokenAsync(user);
                return Ok(new { token = generatedToken });
            }
            return BadRequest();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser? user = await _userManager.FindByEmailAsync(registerModel.Email);

            if (user != null)
            {
                return Conflict("User with this email already exists");
            }

            user = new ApplicationUser
            {
                UserName = registerModel.Email,
                Email = registerModel.Email
            };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
