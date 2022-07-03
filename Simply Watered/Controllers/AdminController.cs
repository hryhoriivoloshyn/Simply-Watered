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
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == userId);
            var adminRoleId = await _context.Roles.FirstOrDefaultAsync(i => i.Name == "admin");
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(i => i.UserId == userId);
            if (userRole == null)
            {
                return Ok(false);
            }
            if (userRole.RoleId == adminRoleId.Id)
            {
                return Ok(true);
            }
            return Ok(false);
        }
    }
}
