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

        public class DeleteModel
        {
            public int id { get; set; }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteModel groupModel)
        {
            var groupId = groupModel.id;
            if (groupId != null)
            {
                RegionGroups group = _context.RegionGroups.FirstOrDefault(g => g.RegionGroupId == groupId);
                if (group != null)
                {
                    _context.RegionGroups.Remove(group);
                    await _context.SaveChangesAsync();
                    return Ok(groupModel);
                }

            }

            return NotFound();
            
        }

        
       
    }
}
