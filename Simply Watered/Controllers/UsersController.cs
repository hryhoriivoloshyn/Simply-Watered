using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Simply_Watered.Data;
using Simply_Watered.Models;

namespace Simply_Watered.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }

        [HttpGet]
        public async Task<IEnumerable<ApplicationUser>> Get()
        {
       
            IEnumerable<ApplicationUser> users = await _context.Users.ToListAsync();
            return users;
        }

        public class InputModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InputModel Input)
        {
            var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                return Ok(Input);
            }

            return BadRequest();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
          
            var result= await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(id);
                
            }
            return BadRequest(id);

        }
    }
}
