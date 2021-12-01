using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Simply_Watered.Data;
using Simply_Watered.Models;
using Simply_Watered.ViewModels;

namespace Simply_Watered.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RegionGroupsController : ControllerBase
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


       

        public class InputModel
        {
            public string GroupName { get; set; }
            public string RegionGroupDescription { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InputModel inputModel)
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

    

        [HttpDelete("{Id:long}")]
        public async Task<IActionResult> Delete(long Id)
        {
            
            var groupId = Id;
            if (groupId != null)
            {
                RegionGroups group = _context.RegionGroups.FirstOrDefault(g => g.RegionGroupId == groupId);
                if (group != null)
                {
                    _context.RegionGroups.Remove(group);
                    await _context.SaveChangesAsync();
                    return Ok(Id);
                }

            }

            return NotFound();
            
        }

        
       
    }
}
