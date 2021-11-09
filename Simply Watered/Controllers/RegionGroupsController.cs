using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Simply_Watered.Data;
using Simply_Watered.Models;

namespace Simply_Watered.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RegionGroupsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegionGroupsController> _logger;

        public RegionGroupsController(ILogger<RegionGroupsController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }


        [HttpGet]
        public IEnumerable<RegionGroups> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<RegionGroups> groups = _context.RegionGroups.Where(g => g.UserId == userId).ToArray();
            return groups;
        }

        [HttpPost]
        public IActionResult Post(RegionGroups regionGroup)
        {
            _context.RegionGroups.Update(regionGroup);
            _context.SaveChangesAsync();
            return Ok(regionGroup);
        }

        [HttpPost]
        public IActionResult Delete(int? groupId)
        {
            if (groupId != null)
            {
                RegionGroups group = _context.RegionGroups.FirstOrDefault(g => g.RegionGroupId == groupId);
                if (group != null)
                {
                    _context.RegionGroups.Remove(group);
                    _context.SaveChangesAsync();
                    return Ok(group);
                }
                   
            }

            return NotFound();
        }

    }
}
