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
    [Route("[controller]")]
    public class RegionsController : Controller 
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(ILogger<RegionsController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }

        public class GroupIdModel
        {
            public int id { get; set; }
        }

        [HttpPost("load")]
        public async Task<JsonResult> LoadRegions([FromBody] GroupIdModel groupIdModel)
        {
            var groupId = groupIdModel.id;
            if (groupId != null)
            {
                IEnumerable<Regions> regions = _context.Regions.Where(r => r.RegionGroupId == groupId).ToArray();
                RegionGroups regionGroup = await _context.RegionGroups.Where(g => g.RegionGroupId == groupId).FirstOrDefaultAsync();
                RegionsViewModel viewModel = new RegionsViewModel()
                {
                    Regions = regions,
                    RegionGroup = regionGroup
                };
                JsonResult response = Json(viewModel);
                return response;
            }

            return null;
        }

        public class DeleteRegionModel
        {
            
            public long id { get; set; }
        }
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteRegionModel regionModel)
        {
            
            var regionId = regionModel.id;
            if (regionModel != null)
            {
                Regions region = _context.Regions.FirstOrDefault(r => r.RegionId == regionId);
                if (region != null)
                {
                    _context.Regions.Remove(region);
                    await _context.SaveChangesAsync();
                    return Ok(regionModel);
                }

            }

            return NotFound();

        }

        public class InputModel
        {
            public string RegionName { get; set; }
            public string RegionDescription { get; set; }
            public long GroupId { get; set; }
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InputModel inputModel)
        {
            if (inputModel != null)
            {
                
                Regions newRegion = new Regions
                {
                    RegionName = inputModel.RegionName,
                    RegionDescription = inputModel.RegionDescription,
                    RegionGroupId = inputModel.GroupId

                };
                _context.Regions.Add(newRegion);
                await _context.SaveChangesAsync();
                return Ok(inputModel);
            }
            return NotFound();
        }


    }
}
