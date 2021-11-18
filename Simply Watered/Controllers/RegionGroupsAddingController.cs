using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Simply_Watered.Data;
using Simply_Watered.Models;

namespace Simply_Watered.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RegionGroupsAddingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegionGroupsAddingController> _logger;

        public RegionGroupsAddingController(ILogger<RegionGroupsAddingController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }

        public class InputModel
        {
            public string GroupName { get; set; }
            public string RegionGroupDescription { get; set; }
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InputModel inputModel)
        {
            if (inputModel != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return NotFound($"Unable to load user with ID '{userId}'.");
                }
                RegionGroups newGroup = new RegionGroups
                {
                    GroupName = inputModel.GroupName,
                    RegionGroupDescription = inputModel.RegionGroupDescription,
                    UserId = userId
                    
                };
                _context.RegionGroups.Add(newGroup);
                await _context.SaveChangesAsync();
                return Ok(inputModel);
            }
            return NotFound();
        }
    }
}
